using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GlowGlowHack
{
    public class WallHack
    {
        public delegate void WallHackErroHandler(Exception e);

        private VAMemory Memory;
        private Thread MainThread;
        public  WallHackErroHandler ErrorHandler;
        public bool IsRunning;

        public WallHack()
        {
            Memory = new VAMemory("csgo"); 
        }

        public void Start()
        {
            MainThread = new Thread(mainThreadLoop);
            MainThread.Start();
            IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
            MainThread.Abort();
        }

        private void mainThreadLoop()
        {
            try
            {
                int baseAddress = 0;

                if (GetClientBaseAddress("csgo", ref baseAddress))
                {
                    while (true)
                    {
                        int playerAddress = baseAddress + Offsets.signatures.dwLocalPlayer; //Endereço do jogador
                        int playerObjectAddress = Memory.ReadInt32((IntPtr)playerAddress); //Acessa o endereço do jogador e retorna o ponteiro do objecto jogador

                        int teamObjectAddress = playerObjectAddress + Offsets.netvars.m_iTeamNum;
                        int playerTeamId = Memory.ReadInt32((IntPtr)teamObjectAddress);

                        /*if (playerTeamId == 3)
                        {
                            Console.Write("Sou CT");
                        }
                        else if (playerTeamId == 2)
                        {
                            Console.Write("Sou TERROR");
                        }*/

                        for (var i = 0; i < 64; i++) //for 0 to 63 (each player)
                        {
                            int entityListAddress = baseAddress + Offsets.signatures.dwEntityList + (i - 1) * 0x10;
                            int entityListObjectAddress = Memory.ReadInt32((IntPtr)entityListAddress);

                            int entity_teamObjectAddress = entityListObjectAddress + Offsets.netvars.m_iTeamNum;
                            int entity_teamId = Memory.ReadInt32((IntPtr)entity_teamObjectAddress);

                            /* if (entity_teamId == 3)
                             {
                                 Console.Write("Ele é CT");
                             }
                             else if (entity_teamId == 2)
                             {
                                 Console.Write("Ele é TERROR");
                             }*/

                            if (entity_teamId != playerTeamId) //Se for do time adversário
                            {
                                int glowIndexAddress = entityListObjectAddress + Offsets.netvars.m_iGlowIndex;
                                int glowIndexObjectAddress = Memory.ReadInt32((IntPtr)glowIndexAddress);

                                int address = baseAddress + Offsets.signatures.dwGlowObjectManager;
                                int GlowObject = Memory.ReadInt32((IntPtr)address);

                                int calculation = glowIndexObjectAddress * 0x38 + 0x4; //Move para o objecto, no ponteiro do valor
                                int current = GlowObject + calculation;
                                Memory.WriteFloat((IntPtr)current, 1); //Red

                                calculation = glowIndexObjectAddress * 0x38 + 0x8;
                                current = GlowObject + calculation;
                                Memory.WriteFloat((IntPtr)current, 0); //Green

                                calculation = glowIndexObjectAddress * 0x38 + 0xc;
                                current = GlowObject + calculation;
                                Memory.WriteFloat((IntPtr)current, 0); //Blue


                                calculation = glowIndexObjectAddress * 0x38 + 0x10;
                                current = GlowObject + calculation;
                                Memory.WriteFloat((IntPtr)current, 1); //Alpha

                                calculation = glowIndexObjectAddress * 0x38 + 0x24;
                                current = GlowObject + calculation;
                                Memory.WriteBoolean((IntPtr)current, true); //RWO

                                calculation = glowIndexObjectAddress * 0x38 + 0x25;
                                current = GlowObject + calculation;
                                Memory.WriteBoolean((IntPtr)current, true); //RWUO
                            }
                        }
                        Thread.Sleep(15);
                    }
                }
                else
                {
                    throw new Exception("CSGO process not founded!");
                }
            }catch(Exception e)
            {
                ErrorHandler?.Invoke(e);
            }
        }

        private bool GetClientBaseAddress(string processName, ref int clientBaseAddress)
        {
            try
            {
                Process[] process = Process.GetProcessesByName(processName);
                if (process.Length > 0)
                {
                    foreach (ProcessModule module in process[0].Modules)
                    {
                        if (module.ModuleName.Equals("client.dll"))
                        {
                            clientBaseAddress = (int)module.BaseAddress;
                            return true;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
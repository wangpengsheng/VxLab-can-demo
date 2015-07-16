/* 
 * Written by Jacob Jacky Aharon @JJAharon
 *          July 2015
 * Robots Sync Demo RMIT 
 *            Vxlab
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ABB.Robotics;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.RapidDomain;
using ABB.Robotics.Controllers.MotionDomain;
using System.Net;
using System.Threading;
using ABB.Robotics.Controllers.EventLogDomain;
using System.IO;

namespace Extended_Robots_Can_Demo
{
    public partial class Form1 : Form
    {
        private NetworkScanner scanner = null;

        private Controller controller_100 = null;
        private Controller controller_101 = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void stop_robots()
        {

            this.controller_100.Rapid.Stop(ABB.Robotics.Controllers.RapidDomain.StopMode.Immediate);

            
            this.controller_101.Rapid.Stop(ABB.Robotics.Controllers.RapidDomain.StopMode.Immediate);

        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView_left_robots.Items.Clear();
            listView_left_robots.Visible=true;
            listView_right_robots.Items.Clear();
            listView_right_robots.Visible=true;
            this.label_left.Text = "Choose Left Controller";
            this.label_right.Text = "Choose Right Controller";
            if (this.controller_100 != null )
            {
                try 
                {
                    this.controller_100.Logoff();
                    this.controller_100 = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (this.controller_101 != null)
            {
                try
                {
                    this.controller_101.Logoff();
                    this.controller_101 = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            listView_left_robots.Enabled=true;
            listView_right_robots.Enabled=true;

            this.scanner = new NetworkScanner();
            this.scanner.Scan();
            ControllerInfoCollection controllers = scanner.Controllers;
            ListViewItem item = null;
            foreach (ControllerInfo controllerInfo in controllers)
            {
                item = new ListViewItem(controllerInfo.IPAddress.ToString());
                item.SubItems.Add(controllerInfo.Availability.ToString());
                item.SubItems.Add(controllerInfo.IsVirtual.ToString());
                item.SubItems.Add(controllerInfo.SystemName);
                item.SubItems.Add(controllerInfo.Version.ToString());
                item.SubItems.Add(controllerInfo.ControllerName);
                this.listView1.Items.Add(item);
                item.Tag = controllerInfo;


                Console.WriteLine("IP: " + controllerInfo.IPAddress.ToString() + " Sys: " + controllerInfo.SystemName);

                //left controllers
                if ((controllerInfo.IsVirtual) && (controllerInfo.SystemName == "J-L-IRB_120_3kg_0.58m") &&
                                                        (controllerInfo.IPAddress.ToString() == "127.0.0.1") 
                                                        ||
                 (!(controllerInfo.IsVirtual) && (controllerInfo.IPAddress.ToString() == "10.234.3.100")))
                {
                    item = new ListViewItem(controllerInfo.IPAddress.ToString());
                    item.SubItems.Add(controllerInfo.Availability.ToString());
                    item.SubItems.Add(controllerInfo.IsVirtual.ToString());
                    item.SubItems.Add(controllerInfo.SystemName);
                    item.SubItems.Add(controllerInfo.Version.ToString());
                    item.SubItems.Add(controllerInfo.ControllerName);
                    this.listView_left_robots.Items.Add(item);
                    item.Tag = controllerInfo;
                }

                //right controllers
                if ((controllerInfo.IsVirtual) && (controllerInfo.SystemName == "J-R-IRB_120_3kg_0.58m") &&
                                                        (controllerInfo.IPAddress.ToString() == "127.0.0.1")
                                                        ||
                 (!(controllerInfo.IsVirtual) && (controllerInfo.IPAddress.ToString() == "10.234.3.101")))
                {
                    item = new ListViewItem(controllerInfo.IPAddress.ToString());
                    item.SubItems.Add(controllerInfo.Availability.ToString());
                    item.SubItems.Add(controllerInfo.IsVirtual.ToString());
                    item.SubItems.Add(controllerInfo.SystemName);
                    item.SubItems.Add(controllerInfo.Version.ToString());
                    item.SubItems.Add(controllerInfo.ControllerName);
                    this.listView_right_robots .Items.Add(item);
                    item.Tag = controllerInfo;
                }



            }
        }



        private void RunController_101(bool run)
        {
            Bool rapidFlag;
            RapidData flagHomed_101 = controller_101.Rapid.GetRapidData("T_ROB1", "Module1", "flagRun");

            if (flagHomed_101.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
            {
                rapidFlag = (ABB.Robotics.Controllers.RapidDomain.Bool)flagHomed_101.Value;
                //assign the value of the RAPID data to a local variable

                rapidFlag.Value = run;
                using (Mastership.Request(controller_101.Rapid))
                {
                    flagHomed_101.Value = rapidFlag;
                }
            }
        }

        private void RunController_100(bool run)
        {
            Bool rapidFlag;
            RapidData flagHomed_100 = controller_100.Rapid.GetRapidData("T_ROB1", "Module1", "flagRun");

            if (flagHomed_100.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
            {
                rapidFlag = (ABB.Robotics.Controllers.RapidDomain.Bool)flagHomed_100.Value;
                //assign the value of the RAPID data to a local variable

                rapidFlag.Value = run;
                using (Mastership.Request(controller_100.Rapid))
                {
                    flagHomed_100.Value = rapidFlag;
                }
            }
        }

        private void StopController_100()
        {
            try
            {
                if (controller_100 != null)
                {
                    using (Mastership.Request(controller_100.Rapid))
                    {
                        controller_100.Rapid.Stop(StopMode.Instruction);
                    }
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StopController_101()
        {
            try
            {
                if (controller_101 != null)
                {
                    using (Mastership.Request(controller_101.Rapid))
                    {
                        controller_101.Rapid.Stop(StopMode.Instruction);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_coordMove_StopRobots_Click(object sender, EventArgs e)
        {
            StopController_100();
            StopController_101();
        }

        private void RunRobots()
        {
            try
            {
                if (controller_100 != null)
                {
                    using (Mastership.Request(controller_100.Rapid))
                    {
                        controller_100.Rapid.Start();
                    }
                }

                if (controller_101 != null)
                {
                    using (Mastership.Request(controller_101.Rapid))
                    {
                        controller_101.Rapid.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetPP_toMain()
        {
            ABB.Robotics.Controllers.RapidDomain.Task[] tasks = this.controller_100.Rapid.GetTasks();

            using (Mastership.Request(this.controller_100.Rapid))
            {
                //this.master = Mastership.Request(this.controller.Rapid);
                tasks[0].SetProgramPointer("Module1", "main");
                //  this.controller_100.Rapid.Start();

                //Release mastership as soon as possible
                //this.master.Dispose();                   
            }

            tasks = this.controller_101.Rapid.GetTasks();
            using (Mastership.Request(this.controller_101.Rapid))
            {
                //this.master = Mastership.Request(this.controller.Rapid);
                tasks[0].SetProgramPointer("Module1", "main");
                // this.controller_101.Rapid.Start();
                //Release mastership as soon as possible
                //this.master.Dispose();                                  
            }
        }
        private void button_coordMove_StartRobot_Click(object sender, EventArgs e)
        {
            this.button_Start.Enabled = false;
            try
            {
                RegisterForEvents();
                SetPP_toMain();
                RunRobots();
                button_Stop.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        bool isRinReadyPosA;
        bool isRinCrossPos;
        bool isRGripperOpened;

        bool isLinCrossPos;
        bool isLGripperClosed;


        RapidData RinReadyPosAEvent;
        RapidData RinCrossPosEvent;
        RapidData RGripperOpenedEvent;

        RapidData LinCrossPosEvent;
        RapidData LGripperClosedEvent;

        //Reverse

        bool R_isRinCrossPos;
        bool R_isRGripperClosed;

        bool R_isLinReadyPosA;
        bool R_isLinCrossPos;
        bool R_isLGripperOpened;

        RapidData R_RinCrossPosEvent;
        RapidData R_RGripperClosedEvent;

        RapidData R_LinReadyPosAEvent;
        RapidData R_LinCrossPosEvent;
        RapidData R_LGripperOpenedEvent;





        private void Sync0()
        {
           
            if (isLinCrossPos)
            {
                isLinCrossPos = false; //TODO: check if there is a better way to use flags as we are setting this to false 
                //but the actual robot might still be in the cross position, if we will use isLinCrossPos in the future it might be dirty 

                Bool rapidFlag;

                try
                {
                    RapidData rd = this.controller_101.Rapid.GetRapidData("T_ROB1", "Module1", "SYNC_CROSS");

                    if (rd.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
                    {
                        rapidFlag = (ABB.Robotics.Controllers.RapidDomain.Bool)rd.Value;
                        //assign the value of the RAPID data to a local variable
                        //bool boolValue = rapidFlag.Value;
                        //MessageBox.Show(rapidFlag.Value.ToString());
                        rapidFlag.Value = true;
                        using (Mastership.Request(this.controller_101.Rapid))
                        {
                            //this.master = Mastership.Request(this.controller.Rapid);
                            //Change: controller is repaced by aController
                            rd.Value = rapidFlag;
                            //Release mastership as soon as possible
                            //this.master.Dispose();                   
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (isLGripperClosed)
            {
                isLGripperClosed = false; //same as above
                Bool rapidFlag;

                try
                {
                    RapidData rd = this.controller_101.Rapid.GetRapidData("T_ROB1", "Module1", "SYNC_GRIPPER");

                    if (rd.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
                    {
                        rapidFlag = (ABB.Robotics.Controllers.RapidDomain.Bool)rd.Value;
                        //assign the value of the RAPID data to a local variable
                        //bool boolValue = rapidFlag.Value;
                        //MessageBox.Show(rapidFlag.Value.ToString());
                        rapidFlag.Value = true;
                        using (Mastership.Request(this.controller_101.Rapid))
                        {
                            //this.master = Mastership.Request(this.controller.Rapid);
                            //Change: controller is repaced by aController
                            rd.Value = rapidFlag;
                            //Release mastership as soon as possible
                            //this.master.Dispose();                   
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        


        private void Sync1()
        {
            if (isRinReadyPosA)
            {
                isRinReadyPosA = false; //same as above
                Bool rapidFlag;

                try
                {
                    RapidData rd = this.controller_100.Rapid.GetRapidData("T_ROB1", "Module1", "SYNC_CROSS");

                    if (rd.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
                    {
                        rapidFlag = (ABB.Robotics.Controllers.RapidDomain.Bool)rd.Value;
                        //assign the value of the RAPID data to a local variable
                        //bool boolValue = rapidFlag.Value;
                        //MessageBox.Show(rapidFlag.Value.ToString());
                        rapidFlag.Value = true;
                        using (Mastership.Request(this.controller_100.Rapid))
                        {
                            //this.master = Mastership.Request(this.controller.Rapid);
                            //Change: controller is repaced by aController
                            rd.Value = rapidFlag;
                            //Release mastership as soon as possible
                            //this.master.Dispose();                   
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            else if (isRinCrossPos)
            {
                isRinCrossPos = false;
                Bool rapidFlag;

                try
                {
                    RapidData rd = this.controller_100.Rapid.GetRapidData("T_ROB1", "Module1", "SYNC_GRIPPER");

                    if (rd.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
                    {
                        rapidFlag = (ABB.Robotics.Controllers.RapidDomain.Bool)rd.Value;
                        //assign the value of the RAPID data to a local variable
                        //bool boolValue = rapidFlag.Value;
                        //MessageBox.Show(rapidFlag.Value.ToString());
                        rapidFlag.Value = true;
                        using (Mastership.Request(this.controller_100.Rapid))
                        {
                            //this.master = Mastership.Request(this.controller.Rapid);
                            //Change: controller is repaced by aController
                            rd.Value = rapidFlag;
                            //Release mastership as soon as possible
                            //this.master.Dispose();                   
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (isRGripperOpened)
            {
                isRGripperOpened = false;
                Bool rapidFlag;

                try
                {
                    RapidData rd = this.controller_100.Rapid.GetRapidData("T_ROB1", "Module1", "SYNC_READY");

                    if (rd.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
                    {
                        rapidFlag = (ABB.Robotics.Controllers.RapidDomain.Bool)rd.Value;
                        //assign the value of the RAPID data to a local variable
                        //bool boolValue = rapidFlag.Value;
                        //MessageBox.Show(rapidFlag.Value.ToString());
                        rapidFlag.Value = true;
                        using (Mastership.Request(this.controller_100.Rapid))
                        {
                            //this.master = Mastership.Request(this.controller.Rapid);
                            //Change: controller is repaced by aController
                            rd.Value = rapidFlag;
                            //Release mastership as soon as possible
                            //this.master.Dispose();                   
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //Reverse
        private void R_Sync0()
        {

            if (R_isRinCrossPos)
            {
                R_isRinCrossPos = false; //TODO: check if there is a better way to use flags as we are setting this to false 
                //but the actual robot might still be in the cross position, if we will use isLinCrossPos in the future it might be dirty 

                Bool rapidFlag;

                try
                {
                    RapidData rd = this.controller_100.Rapid.GetRapidData("T_ROB1", "Module1", "R_SYNC_CROSS");

                    if (rd.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
                    {
                        rapidFlag = (ABB.Robotics.Controllers.RapidDomain.Bool)rd.Value;
                        //assign the value of the RAPID data to a local variable
                        //bool boolValue = rapidFlag.Value;
                        //MessageBox.Show(rapidFlag.Value.ToString());
                        rapidFlag.Value = true;
                        using (Mastership.Request(this.controller_100.Rapid))
                        {
                            //this.master = Mastership.Request(this.controller.Rapid);
                            //Change: controller is repaced by aController
                            rd.Value = rapidFlag;
                            //Release mastership as soon as possible
                            //this.master.Dispose();                   
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (R_isRGripperClosed)
            {
                R_isRGripperClosed = false; //same as above
                Bool rapidFlag;

                try
                {
                    RapidData rd = this.controller_100.Rapid.GetRapidData("T_ROB1", "Module1", "R_SYNC_GRIPPER");

                    if (rd.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
                    {
                        rapidFlag = (ABB.Robotics.Controllers.RapidDomain.Bool)rd.Value;
                        //assign the value of the RAPID data to a local variable
                        //bool boolValue = rapidFlag.Value;
                        //MessageBox.Show(rapidFlag.Value.ToString());
                        rapidFlag.Value = true;
                        using (Mastership.Request(this.controller_100.Rapid))
                        {
                            //this.master = Mastership.Request(this.controller.Rapid);
                            //Change: controller is repaced by aController
                            rd.Value = rapidFlag;
                            //Release mastership as soon as possible
                            //this.master.Dispose();                   
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }



        private void R_Sync1()
        {
            if (R_isLinReadyPosA)
            {
                R_isLinReadyPosA = false; //same as above
                Bool rapidFlag;

                try
                {
                    RapidData rd = this.controller_101.Rapid.GetRapidData("T_ROB1", "Module1", "R_SYNC_CROSS");

                    if (rd.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
                    {
                        rapidFlag = (ABB.Robotics.Controllers.RapidDomain.Bool)rd.Value;
                        //assign the value of the RAPID data to a local variable
                        //bool boolValue = rapidFlag.Value;
                        //MessageBox.Show(rapidFlag.Value.ToString());
                        rapidFlag.Value = true;
                        using (Mastership.Request(this.controller_101.Rapid))
                        {
                            //this.master = Mastership.Request(this.controller.Rapid);
                            //Change: controller is repaced by aController
                            rd.Value = rapidFlag;
                            //Release mastership as soon as possible
                            //this.master.Dispose();                   
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            else if (R_isLinCrossPos)
            {
                isRinCrossPos = false;
                Bool rapidFlag;

                try
                {
                    RapidData rd = this.controller_101.Rapid.GetRapidData("T_ROB1", "Module1", "R_SYNC_GRIPPER");

                    if (rd.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
                    {
                        rapidFlag = (ABB.Robotics.Controllers.RapidDomain.Bool)rd.Value;
                        //assign the value of the RAPID data to a local variable
                        //bool boolValue = rapidFlag.Value;
                        //MessageBox.Show(rapidFlag.Value.ToString());
                        rapidFlag.Value = true;
                        using (Mastership.Request(this.controller_101.Rapid))
                        {
                            //this.master = Mastership.Request(this.controller.Rapid);
                            //Change: controller is repaced by aController
                            rd.Value = rapidFlag;
                            //Release mastership as soon as possible
                            //this.master.Dispose();                   
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (R_isLGripperOpened)
            {
                isRGripperOpened = false;
                Bool rapidFlag;

                try
                {
                    RapidData rd = this.controller_101.Rapid.GetRapidData("T_ROB1", "Module1", "R_SYNC_READY");

                    if (rd.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
                    {
                        rapidFlag = (ABB.Robotics.Controllers.RapidDomain.Bool)rd.Value;
                        //assign the value of the RAPID data to a local variable
                        //bool boolValue = rapidFlag.Value;
                        //MessageBox.Show(rapidFlag.Value.ToString());
                        rapidFlag.Value = true;
                        using (Mastership.Request(this.controller_101.Rapid))
                        {
                            //this.master = Mastership.Request(this.controller.Rapid);
                            //Change: controller is repaced by aController
                            rd.Value = rapidFlag;
                            //Release mastership as soon as possible
                            //this.master.Dispose();                   
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        
        private void clear_flags()
        {
            isRinReadyPosA = false;
            isRinCrossPos = false;
            isRGripperOpened = false;

            isLinCrossPos = false;
            isLGripperClosed = false;

            //Reverse
            R_isRinCrossPos = false;
            R_isRGripperClosed = false;

            R_isLinReadyPosA = false;
            R_isLinCrossPos = false;
            R_isLGripperOpened = false;
 
        }
        private void RegisterForEvents()
        {

            clear_flags();
            //controller_101 RHS
            RinReadyPosAEvent = controller_101.Rapid.GetRapidData("T_ROB1", "Module1", "isRinReadyPosA");
            if (RinReadyPosAEvent != null)
            {
                RinReadyPosAEvent.ValueChanged += RinReadyPos_EventHandler;
            }
            RinCrossPosEvent = controller_101.Rapid.GetRapidData("T_ROB1", "Module1", "isRinCrossPos");
            if (RinCrossPosEvent != null)
            {
                RinCrossPosEvent.ValueChanged += RinCrossPos_EventHandler;
            }
            RGripperOpenedEvent = controller_101.Rapid.GetRapidData("T_ROB1", "Module1", "isRGripperOpened");
            if (RGripperOpenedEvent != null)
            {
                RGripperOpenedEvent.ValueChanged += RGripperOpened_EventHandler;
            }

            //controller_100 LHS
            LinCrossPosEvent = controller_100.Rapid.GetRapidData("T_ROB1", "Module1", "isLinCrossPos");
            if (LinCrossPosEvent != null)
            {
                LinCrossPosEvent.ValueChanged += LinCrossPos_EventHandler;
            }
            LGripperClosedEvent = controller_100.Rapid.GetRapidData("T_ROB1", "Module1", "isLGripperClosed");
            if (LGripperClosedEvent != null)
            {
                LGripperClosedEvent.ValueChanged += LGripperClosed_EventHandler;
            }
            
            //Reverse
            //controller_101 RHS
            R_RinCrossPosEvent = controller_101.Rapid.GetRapidData("T_ROB1", "Module1", "R_isRinCrossPos");
            if (R_RinCrossPosEvent != null)
            {
                R_RinCrossPosEvent.ValueChanged += R_RinCrossPos_EventHandler;
            }
            R_RGripperClosedEvent = controller_101.Rapid.GetRapidData("T_ROB1", "Module1", "R_isRGripperClosed");
            if (R_RGripperClosedEvent != null)
            {
                R_RGripperClosedEvent.ValueChanged += R_RinGripperClosed_EventHandler;
            }

            //controller_100 LHS
            R_LinReadyPosAEvent = controller_100.Rapid.GetRapidData("T_ROB1", "Module1", "R_isLinReadyPosA");
            if (R_LinReadyPosAEvent != null)
            {
                R_LinReadyPosAEvent.ValueChanged += R_LinReadyPos_EventHandler;
            }
            R_LinCrossPosEvent = controller_100.Rapid.GetRapidData("T_ROB1", "Module1", "R_isLinCrossPos");
            if (R_LinCrossPosEvent != null)
            {
                R_LinCrossPosEvent.ValueChanged += R_LinCrossPos_EventHandler;
            }
            R_LGripperOpenedEvent = controller_100.Rapid.GetRapidData("T_ROB1", "Module1", "R_isLGripperOpened");
            if (R_LGripperOpenedEvent != null)
            {
                R_LGripperOpenedEvent.ValueChanged += R_LGripperOpened_EventHandler;
            }
        }

        private void RinReadyPos_EventHandler(object sender, DataValueChangedEventArgs e)
        {
            this.Invoke(new EventHandler(RinReadyPos), new Object[] { sender, e });
        }

        private void RinReadyPos(Object sender, EventArgs e)
        {
            Bool rapidBool;

            if (RinReadyPosAEvent.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
            {
                rapidBool = (ABB.Robotics.Controllers.RapidDomain.Bool)RinReadyPosAEvent.Value;
                isRinReadyPosA = rapidBool.Value;
                Sync1();
            }
        }

        private void RinCrossPos_EventHandler(object sender, DataValueChangedEventArgs e)
        {
            this.Invoke(new EventHandler(RinCrossPos), new Object[] { sender, e });
        }

        private void RinCrossPos(Object sender, EventArgs e)
        {
            Bool rapidBool;

            if (RinCrossPosEvent.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
            {
                rapidBool = (ABB.Robotics.Controllers.RapidDomain.Bool)RinCrossPosEvent.Value;
                isRinCrossPos = rapidBool.Value;
                Sync1();
            }
        }

        private void RGripperOpened_EventHandler(object sender, DataValueChangedEventArgs e)
        {
            this.Invoke(new EventHandler(RGripperOpened), new Object[] { sender, e });
        }

        private void RGripperOpened(Object sender, EventArgs e)
        {
            Bool rapidBool;

            if (RGripperOpenedEvent.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
            {
                rapidBool = (ABB.Robotics.Controllers.RapidDomain.Bool)RGripperOpenedEvent.Value;
                isRGripperOpened = rapidBool.Value;
                Sync1();
            }
        }


        private void LinCrossPos_EventHandler(object sender, DataValueChangedEventArgs e)
        {
            this.Invoke(new EventHandler(LinCrossPos), new Object[] { sender, e });
        }

        private void LinCrossPos(Object sender, EventArgs e)
        {
            Bool rapidBool;

            if (LinCrossPosEvent.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
            {
                rapidBool = (ABB.Robotics.Controllers.RapidDomain.Bool)LinCrossPosEvent.Value;
                isLinCrossPos = rapidBool.Value;
                Sync0();
            }
        }

        private void LGripperClosed_EventHandler(object sender, DataValueChangedEventArgs e)
        {
            this.Invoke(new EventHandler(LGripperClosed), new Object[] { sender, e });
        }

        private void LGripperClosed(Object sender, EventArgs e)
        {
            Bool rapidBool;

            if (LGripperClosedEvent.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
            {
                rapidBool = (ABB.Robotics.Controllers.RapidDomain.Bool)LGripperClosedEvent.Value;
                isLGripperClosed = rapidBool.Value;
                Sync0();
            }
        }

        //Reverse
        private void R_LinReadyPos_EventHandler(object sender, DataValueChangedEventArgs e)
        {
            this.Invoke(new EventHandler(R_LinReadyPos), new Object[] { sender, e });
        }

        private void R_LinReadyPos(Object sender, EventArgs e)
        {
            Bool rapidBool;

            if (R_LinReadyPosAEvent.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
            {
                rapidBool = (ABB.Robotics.Controllers.RapidDomain.Bool)R_LinReadyPosAEvent.Value;
                R_isLinReadyPosA = rapidBool.Value;
                R_Sync1();
            }
        }

        private void R_LinCrossPos_EventHandler(object sender, DataValueChangedEventArgs e)
        {
            this.Invoke(new EventHandler(R_LinCrossPos), new Object[] { sender, e });
        }

        private void R_LinCrossPos(Object sender, EventArgs e)
        {
            Bool rapidBool;

            if (R_LinCrossPosEvent.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
            {
                rapidBool = (ABB.Robotics.Controllers.RapidDomain.Bool)R_LinCrossPosEvent.Value;
                R_isLinCrossPos = rapidBool.Value;
                R_Sync1();
            }
        }

        private void R_LGripperOpened_EventHandler(object sender, DataValueChangedEventArgs e)
        {
            this.Invoke(new EventHandler(R_LGripperOpened), new Object[] { sender, e });
        }

        private void R_LGripperOpened(Object sender, EventArgs e)
        {
            Bool rapidBool;

            if (R_LGripperOpenedEvent.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
            {
                rapidBool = (ABB.Robotics.Controllers.RapidDomain.Bool)R_LGripperOpenedEvent.Value;
                R_isLGripperOpened = rapidBool.Value;
                R_Sync1();
            }
        }


        private void R_RinCrossPos_EventHandler(object sender, DataValueChangedEventArgs e)
        {
            this.Invoke(new EventHandler(R_RinCrossPos), new Object[] { sender, e });
        }

        private void R_RinCrossPos(Object sender, EventArgs e)
        {
            Bool rapidBool;

            if (R_RinCrossPosEvent.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
            {
                rapidBool = (ABB.Robotics.Controllers.RapidDomain.Bool)R_RinCrossPosEvent.Value;
                R_isRinCrossPos = rapidBool.Value;
                R_Sync0();
            }
        }

        private void R_RinGripperClosed_EventHandler(object sender, DataValueChangedEventArgs e)
        {
            this.Invoke(new EventHandler(R_RGripperClosed), new Object[] { sender, e });
        }

        private void R_RGripperClosed(Object sender, EventArgs e)
        {
            Bool rapidBool;

            if (R_RGripperClosedEvent.Value is ABB.Robotics.Controllers.RapidDomain.Bool)
            {
                rapidBool = (ABB.Robotics.Controllers.RapidDomain.Bool)R_RGripperClosedEvent.Value;
                R_isRGripperClosed = rapidBool.Value;
                R_Sync0();
            }
        }



     
        void EventLog_MessageWritten(object sender, MessageWrittenEventArgs e)
        {
            EventLogMessage msg = e.Message;
            Console.WriteLine(string.Format("Received {0} message from controller.\nTitle: {1}\nBody: {2}",
       msg.Type,
       msg.Title,
       msg.Body));
        }


        private void test_start()
        {
            if ((controller_100!=null) && (controller_101 != null))
            {
                button_Start.Enabled = true;
            }
        }
        private void button_Stop_Click(object sender, EventArgs e)
        {
            try
            {
                stop_robots();
                button_Start.Enabled = true;
                button_Stop.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void listView_right_robots_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            {

                ListViewItem item = this.listView_right_robots.SelectedItems[0]; 
                if (item.Tag != null)
                {
                    try
                    {
                        ControllerInfo controllerInfo = (ControllerInfo)item.Tag; if (controllerInfo.Availability == Availability.Available)
                        {

                            if (controllerInfo.IsVirtual)
                            {
                                if ((controllerInfo.IsVirtual) && (controllerInfo.SystemName == "J-R-IRB_120_3kg_0.58m") &&
                                                             (controllerInfo.IPAddress.ToString() == "127.0.0.1"))
                                {
                                    controller_101 = ControllerFactory.CreateFrom(controllerInfo);
                                    controller_101.Logon(UserInfo.DefaultUser);


                                    MessageBox.Show("Connected to local virtual controller 101 (RIGHT)");
                                }

                            }
                            else //real controller
                            {
                                if (MessageBox.Show("This is NOT a virtual controller, do you really want to connect to that?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                                {

                                    //Real left
                                    if (!(controllerInfo.IsVirtual) && (controllerInfo.IPAddress.ToString() == "10.234.3.101"))
                                    {
                                        controller_101 = ControllerFactory.CreateFrom(controllerInfo);
                                        controller_101.Logon(UserInfo.DefaultUser);


                                        MessageBox.Show("Connected to REAL controller 101 (RIGHT)");
                                    }

                                }
                            }

                            if (this.controller_101 != null)
                            {
                                this.controller_101.Rapid.Stop(ABB.Robotics.Controllers.RapidDomain.StopMode.Immediate);
                                this.listView_right_robots.Visible = false;
                                string real = "Real";
                                if (controllerInfo.IsVirtual)
                                {
                                    real = "Virtual";
                                }
                                this.label_right.Text = "Connected to " + real + "\n" + controllerInfo.SystemName;


                                test_start();
                            }

                        }
                        else
                        {
                            MessageBox.Show("Selected controller not available.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void listView_left_robots_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

            if (this.listView_left_robots.SelectedIndices.Count==0) return;
            ListViewItem item = this.listView_left_robots.SelectedItems[0]; 
            if (item.Tag != null)
            {
                try
                {
                    ControllerInfo controllerInfo = (ControllerInfo)item.Tag; if (controllerInfo.Availability == Availability.Available)
                    {
                        if ((controllerInfo.IsVirtual) && (controllerInfo.SystemName == "J-L-IRB_120_3kg_0.58m") &&
                                                        (controllerInfo.IPAddress.ToString() == "127.0.0.1"))
                        {
                            controller_100 = ControllerFactory.CreateFrom(controllerInfo);
                            controller_100.Logon(UserInfo.DefaultUser);


                            MessageBox.Show("Connected to local virtual controller 100 (LEFT)");
                        }

                        else //real controller
                        {
                            if (MessageBox.Show("This is NOT a virtual controller, do you really want to connect to that?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                            {
                                //Real left
                                if (!(controllerInfo.IsVirtual) && (controllerInfo.IPAddress.ToString() == "10.234.3.100"))
                                {
                                    controller_100 = ControllerFactory.CreateFrom(controllerInfo);
                                    controller_100.Logon(UserInfo.DefaultUser);

                                    MessageBox.Show("Connected to REAL controller 100 (LEFT)");
                                }


                            }
                        }
                        if (this.controller_100 != null)
                        {
                            this.controller_100.Rapid.Stop(ABB.Robotics.Controllers.RapidDomain.StopMode.Immediate);
                            this.listView_left_robots.Visible = false;
                            string real="Real";
                            if (controllerInfo.IsVirtual)
                            {
                                real = "Virtual";
                            }
                            this.label_left.Text = "Connected to " + real +"\n" + controllerInfo.SystemName;
                           

                            test_start();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Selected controller not available.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }







    }
}

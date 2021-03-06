﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using StartUpTemplate;

namespace AnimationStartUp
{
    public partial class CommonAnimationForm : Form
    {
        public StartUpTemplateClass ParentStartUp = null;
        private readonly int TimeOut = 200;
        /// <summary>
        /// 字体前景颜色
        /// </summary>
        public new Color ForeColor
        {
            get => ProgressLabel.ForeColor;
            set => ProgressLabel.ForeColor = value;
        }
        private CommonAnimationForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler((Leon, Mathilda) => { ParentStartUp?.OnStartUpFinished(Mathilda); });
        }

        /// <summary>
        /// 构造播放指定资源的 StartUpForm
        /// </summary>
        /// <param name="imageResource">图像资源</param>
        /// <param name="backColor">背景颜色</param>
        /// <param name="timeOut">每步等待毫秒数（用于控制等候时长）</param> 
        public CommonAnimationForm(Image imageResource, Color backColor, int timeOut = 200) : this()
        {
            this.WindowState = FormWindowState.Maximized;
            
            if (imageResource != null)
            {
                CommonPictureBox.Image = imageResource;
                CommonPictureBox.Size = imageResource.Size;
            }
            this.BackColor = backColor;
            this.TimeOut = timeOut;
        }

        private void RainbowRingForm_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            ThreadPool.QueueUserWorkItem(new WaitCallback(
                (Leon) => {
                    int Progress = 0;
                    try
                    {
                        while (Progress < 100)
                        {
                            Thread.Sleep(TimeOut);
                            Progress += 5;

                            this.Invoke(new Action(() => {
                                ProgressLabel.Text = string.Format("Hack System Loading ... {0}%", Progress);
                                Application.DoEvents();
                            }));
                        }

                        this.Invoke(new Action(() => {
                            ProgressLabel.Text = "Hack System Loaded !\n Welcome. (〃'▽'〃)";
                            Application.DoEvents();
                        }));

                        if (this == null) return;
                        ThreadPool.QueueUserWorkItem(new WaitCallback(
                            (Mathilda) => {
                                try
                                {
                                    while (this.Opacity > 0)
                                    {
                                        Thread.Sleep(100);
                                        this.Opacity -= 0.1;
                                    }

                                    this.Close();
                                }
                                catch
                                { }
                            }));
                    }
                    catch { }
                }));
        }
    }
}

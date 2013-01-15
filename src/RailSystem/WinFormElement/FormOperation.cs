using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WinFormElement
{
    public class FormOperation
    {
        FormShowRegion formShowRegion = new FormShowRegion();
        FormTreeViewOp formTreeViewOp = new FormTreeViewOp();
        //Map<Vehicle> vehicleList = new List<Vehicle>();
        Dictionary<uint, Vehicle> dictVechiles = new Dictionary<uint, Vehicle>();
        [DllImport("kernel32.dll")]
        private static extern int GetTickCount();

        public Int16 canvasOffset = 10;
        private int traceBaseValue = 10;
        private float traceRatio = 1;

        public FormOperation()
        {
        }

        public void FormShowRegionInit(Size showPicSz)
        {
            if (formShowRegion.ReadRailSaveFile())
            {
                formShowRegion.InitRailList();
                AdjustCanvasSize(showPicSz);
            }
        }

        public void AdjustCanvasSize(Size showPicSz)
        {
            formShowRegion.AdjustRailSize(showPicSz);
        }

        public void ShowRegion(Graphics canvas)
        {
            //RemoveLencyOHT();
            canvas.SmoothingMode = SmoothingMode.HighQuality;
            canvas.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            GraphicsContainer moveContainer = canvas.BeginContainer();
            canvas.TranslateTransform(formShowRegion.canvasMoveX, formShowRegion.canvasMoveY);

            GraphicsContainer stretchContainer = canvas.BeginContainer();
            canvas.TranslateTransform(formShowRegion.ptCanStrOffset.X, formShowRegion.ptCanStrOffset.Y);
            canvas.ScaleTransform(traceRatio, traceRatio);

            GraphicsContainer objectContainer = canvas.BeginContainer();

            canvas.TranslateTransform(formShowRegion.ptTranslate.X, formShowRegion.ptTranslate.Y);
            canvas.ScaleTransform(formShowRegion.xScale, formShowRegion.yScale);

            formShowRegion.DrawRailInfo(canvas);
            lock (dictVechiles)
            {
                formShowRegion.DrawVehicleInfo(canvas, dictVechiles);
            }

            canvas.EndContainer(objectContainer);
            canvas.EndContainer(stretchContainer);
            canvas.EndContainer(moveContainer);
            
           
        }

        public void RemoveLencyOHT()
        {
            int nNow = GetTickCount();
            lock (dictVechiles)
            {
                foreach (KeyValuePair<uint, Vehicle> item in dictVechiles)
                {
                    Vehicle oht = item.Value;
                    if (nNow - oht.UpdateTime > 10000)
                    {
                        dictVechiles.Remove(oht.ID);
                    }
                }
            }
           
        }

        public void UpdateOHTPos(List<OhtPos> listOhtPos)
        {
            foreach (OhtPos item in listOhtPos)
            {
                lock (dictVechiles)
                {
                    if (dictVechiles.ContainsKey(item.nID))
                    {
                        Vehicle oht;
                        bool bGet = dictVechiles.TryGetValue(item.nID, out oht);
                        if (bGet)
                        {
                            oht.PosCode = item.nPos;
                            oht.Hand = item.nHand;
                            oht.UpdateTime = GetTickCount();
                        }
                    }
                    else
                    {
                        Vehicle oht = new Vehicle(item.nID);
                        oht.PosCode = item.nPos;
                        oht.Hand = item.nHand;
                        oht.UpdateTime = GetTickCount();
                        dictVechiles.Add(item.nID, oht);
                    }
                }
            }


        }

        

        ////添加第二个参数，表示添加节点类型，根据节点类型调用switch函数
        //public void AddVehicleNode(TreeView tempTreeView)
        //{
        //    Vehicle vehicle = new Vehicle(vehicleID);
        //    vehicleList.Add(vehicle);
        //    formTreeViewOp.AddVehicleNode(tempTreeView, vehicleID);
        //    vehicleID++;
        //}

        public void DeleteVehicleNode(TreeView tempTreeView)
        { }

        public void BtnCanvasMove(string str)
        {
            formShowRegion.CanvasTranslate(str, canvasOffset);
        }

        public void ChangeCanvasScale(int traceValue)
        {
            traceRatio = Convert.ToSingle(traceValue * 1.0 / traceBaseValue);
            formShowRegion.CanvasStretchOffset(traceRatio);
        }


    }
}

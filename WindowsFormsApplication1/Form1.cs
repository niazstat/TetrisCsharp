using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        System.Timers.Timer timer;
        RectObj rect;

        List<RectObj> list = new List<RectObj>();

        List<RectObj> listSuggest = new List<RectObj>();

        static int initStart = 4;
        static int noOfBlockInRow = 10;

        int currentShapeIndex = 0;
        int currentRotationIndex = 0;

        int currentShapeIndexSuggest = 0;
        //int[,] lShape = null;
        // int[,] zShape = null;
        // int[,] tShape = null;
        // int[,] oShape = null;
        // int[,] iShape = null;
        Random random = new Random();
        int[][,] allShapes = new int[5][,];

        int[,] allShapesSuggest;

        bool firstStart = true;
        int score = 0;
        int highScore = 0;
        // InitShapes();
        public Form1()
        {
            InitializeComponent();

            this.KeyUp += Form1_KeyUp;
            timer = new System.Timers.Timer(500);
            timer.Elapsed += Timer_Elapsed;
       
            InitShapes();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                if (!CheckRightFench())
                {
                   
                    UndrawDraw();
                    initStart += 1;
                    // InitShapes();
                    Draw();
                }
            }
           else if (e.KeyCode == Keys.Left)
            {
                if (CheckLeftFench())
                {
                  
                UndrawDraw();
                    initStart -= 1;
                    // InitShapes();
                    Draw();
                    }
            }
           else if (e.KeyCode == Keys.Down)
            {
                MoveDown();
            }

           else if (e.KeyCode == Keys.Up)
            {
                UndrawDraw();

                if (currentRotationIndex ==3 )
                {
                    currentRotationIndex = 0;
                }
                else
                {
                    currentRotationIndex += 1;
                }

           

              //  InitShapes();
                Draw();
            }

            else
            {

            }
        }



        private bool CheckLeftFench()
        {

            if(initStart % noOfBlockInRow == 0)
            {
                return false;
            }

            int[,] att = allShapes[currentShapeIndex];


            //GetLength(0) , return the no of row in 2 dimen array
            //GetLength(1) , return the no of Col in 2 dimen array


            for (int i = 0; i < att.GetLength(1); i++)
            {
               /// int fdfd = att[currentRotationIndex, i];

                if (list[initStart+ att[currentRotationIndex, i] - 1].TagName == "Taken")
                {
                    return false;
                }

            }
            return true;
        }
  



        //int[,] lShape = { { initStart, initStart + 1, initStart + noOfBlockInRow, 2 * noOfBlockInRow + initStart } };

        private void InitShapes()
        {

            // noOfBlockInRow =10 , each row contains 10 block ;
            //L Shape
            allShapes[0] = new int[,]  {
                { 0, 1, noOfBlockInRow, 2 * noOfBlockInRow  },
                {  noOfBlockInRow  ,noOfBlockInRow + 1, noOfBlockInRow + 2,  2 * noOfBlockInRow + 2},
                {   1  , 1 + noOfBlockInRow , noOfBlockInRow * 2, 2 * noOfBlockInRow + 1},
                {  noOfBlockInRow  , 2 *noOfBlockInRow ,  2 *noOfBlockInRow +1,   2 *noOfBlockInRow +2},
            };
            //Z Shapw
            allShapes[1] = new int[,] {
                 { 0   , 1 *noOfBlockInRow , 1 *noOfBlockInRow + 1,   2 *noOfBlockInRow +1},
                 { noOfBlockInRow+1 , noOfBlockInRow+2 ,   2*noOfBlockInRow,     2*noOfBlockInRow+1},
                 { 0   ,  1 *noOfBlockInRow , 1 *noOfBlockInRow + 1,   2 *noOfBlockInRow +1},
                 { noOfBlockInRow+1 , noOfBlockInRow+2 , 2*noOfBlockInRow,   2*noOfBlockInRow+1},
            };

            // T-Shapes
            allShapes[2] = new int[,]{
                 { 1  , 1 *noOfBlockInRow ,  1 *noOfBlockInRow  + 1,    1 *noOfBlockInRow +2},
                 { 1  ,   1 *noOfBlockInRow  + 1,    1 *noOfBlockInRow +2 ,  2 *noOfBlockInRow+1},
                 {  1 *noOfBlockInRow ,   1 *noOfBlockInRow  + 1,   1 *noOfBlockInRow +2,2*noOfBlockInRow+1},
                 { 1  ,  1 *noOfBlockInRow  ,   1 *noOfBlockInRow +1 ,  2 *noOfBlockInRow+1},

          };

            // O-Shapes
            allShapes[3] = new int[,]{
                 { 0   ,  1  ,   1 *noOfBlockInRow  ,   1 *noOfBlockInRow +1},
                 { 0   ,  1  ,   1 *noOfBlockInRow  ,   1 *noOfBlockInRow +1},
            { 0   ,  1  ,   1 *noOfBlockInRow  ,   1 *noOfBlockInRow +1},
                 { 0   ,  1  ,   1 *noOfBlockInRow  ,   1 *noOfBlockInRow +1},

          };



            // I-Shapes

            allShapes[4] = new int[,]{
                 { 0  , 1*noOfBlockInRow   ,  2*noOfBlockInRow   ,   3*noOfBlockInRow },
                 { 1*noOfBlockInRow  , 1*noOfBlockInRow  + 1  ,  1*noOfBlockInRow + 2  ,    1*noOfBlockInRow  + 3 },
                 {  0  , 1*noOfBlockInRow   ,  2*noOfBlockInRow   ,   3*noOfBlockInRow },
                 { 1*noOfBlockInRow  , 1*noOfBlockInRow  + 1  ,  1*noOfBlockInRow + 2  ,    1*noOfBlockInRow  + 3 },

            };


            // 5 , since no of block in each row  for Displaying Suggestion is 5

            allShapesSuggest = new int[,]{
                                         { 0, 1, 5, 2 * 5  },
                                         { 0   , 1 *5 , 1 *5 + 1,   2 *5 +1},
                                         { 1  , 1 *5 ,  1 *5  + 1,    1 *5 +2},
                                        { 0   ,  1  ,   1 *5  ,   1 *5 +1},
                                         { 0  , 1*5   ,  2*5   ,   3*5 }
            };

        }

        private bool CheckRightFench()
        {


            int[,] att =allShapes[currentShapeIndex];

          // int gfgf= att.GetLength(1);
            //for (int i = 0; i < att.GetLength(1); i++)
            //{
            //    int fdfd = (initStart + att[currentRotationIndex,i]) % noOfBlockInRow;

            //    if (((initStart + att[currentRotationIndex,i]) % noOfBlockInRow) == (noOfBlockInRow - 1))
            //    {
            //        return true;
            //    }

            //}
            for (int i = 0; i < att.GetLength(1); i++)
            {
             //   int fdfd = att[currentRotationIndex, i];

                if (( initStart+ att[currentRotationIndex, i]) % noOfBlockInRow == (noOfBlockInRow - 1))
                {
                    return true;
                }

            }

            for (int i = 0; i < att.GetLength(1); i++)
            {
               // int fdfd = att[currentRotationIndex, i];



                if (list[initStart+ att[currentRotationIndex, i] + 1].TagName == "Taken")
                {
                    return true;
                }

            }

            return false;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            MoveDown();

        }


        private void MoveDown()
        {


           // UndrawDrawSuggest();
            //DrawSuggest();

            UndrawDraw();
            // InitShapes();

            if (firstStart)
            {
                // nitStart = nitStart;
                firstStart = false;
            }
            else
            {


                initStart += noOfBlockInRow;
            }

            Draw();
            freeze();
            EndGame();
        }

        private void UndrawDraw()
        {

            for (int i = 0; i < allShapes[0].GetLength(1); i++)
            {
                list[initStart+ allShapes[currentShapeIndex][currentRotationIndex, i]].SetInActive();
            }



        }

        private void Draw()
        {

            for (int i = 0; i < allShapes[0].GetLength(1); i++)
            {
                list[ initStart+ allShapes[currentShapeIndex][currentRotationIndex, i]].SetActive();
            }
        }



        private void UndrawDrawSuggest()
        {

            for (int i = 0; i < allShapesSuggest.GetLength(1); i++)
            {
                listSuggest[2 + allShapesSuggest[currentShapeIndexSuggest, i]].SetInActive();
            }



        }
        private void DrawSuggest()
        {

            for (int i = 0; i < allShapesSuggest.GetLength(1); i++)
            {
                listSuggest[2 + allShapesSuggest[currentShapeIndexSuggest, i]].SetActive();
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            currentShapeIndexSuggest = random.Next(0, 5);
            currentShapeIndex = random.Next(0, 5);



            DrawAllRectangle();
            DrawAllRectangleSuggestion();
            //Rectangle rect = new Rectangle(10, 10, 100, 50);

            button2.Enabled = true;

            // Draw the rectangle using a Graphics object
            // Graphics graphics = this.CreateGraphics(); // this refers to the current form or control
            // Pen pen = new Pen(Color.Black); // create a black pen to draw the outline of the rectangle
            // graphics.DrawRectangle(pen, rect);

        }

        private void DrawAllRectangle()
        {
            int i = 1;
            int _x = 10;
            int _y = 10;
            foreach (var item in list)
            {
                item.DrawRectangle(_x, _y, 20, 20);
                _x += 21;
                if (i % 10 == 0)
                {
                    _x = 10;
                    _y += 21;
                }
                i += 1;
            }
        }

        private void DrawAllRectangleSuggestion()
        {
            int i = 1;
            int _x = 0;
            int _y = 0;
            foreach (var item in listSuggest)
            {
                item.DrawRectangle(_x, _y, 15, 15);
                _x += 16;
                if (i % 5 == 0)
                {
                    _x = 0;
                    _y +=16;
                }
                i += 1;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            InitList();
            // For Displaying Suggestion
         

            // Not Working below Code
            // DrawAllRectangle();

        }


        private void InitList()
        {
            list = new List<RectObj>();
            listSuggest = new List<RectObj>();
            for (int i = 0; i < 200; i++)
            {

                list.Add(new RectObj(this, Color.Red));


            }
            //Create Some Transparent Block
            for (int i = 1; i <= 10; i++)
            {

                list.Add(new RectObj(this, Color.Transparent, "Taken"));



            }

            for (int i = 0; i < 25; i++)
            {

                listSuggest.Add(new RectObj(this.panel1, Color.Red));


            }
        }
        private void UpdateScore(string score)
        {
            if (txtScore.InvokeRequired)
            {
                txtScore.Invoke(new Action(() => txtScore.Text = score));
            }
            else
            {
                txtScore.Text = score;
            }
        }

        private void UpdateHighScore(string score)
        {
            if (txtHighScore.InvokeRequired)
            {
                txtHighScore.Invoke(new Action(() => txtHighScore.Text = score));
            }
            else
            {
                txtHighScore.Text = score;
            }
        }
        private void EnableStarAgaiButton()
        {
            if (btnStartAgain.InvokeRequired)
            {
                txtHighScore.Invoke(new Action(() => btnStartAgain.Enabled = true));
            }
            else
            {
                btnStartAgain.Enabled = true;
            }
        }
        private void DisplayEnd(string score)
        {
            if (txtDisplay.InvokeRequired)
            {
                txtDisplay.Invoke(new Action(() => txtDisplay.Text = score));
            }
            else
            {
                txtDisplay.Text = score;
            }
        }
        private void freeze()
        {
            if (IsEndofRow())
            {
                for (int i = 0; i < allShapes[0].GetLength(1); i++)
                {
                    list[ initStart+ allShapes[currentShapeIndex][currentRotationIndex, i]].TagName = "Taken";

                }



                //cHECK ALL iTEM IN A ROW HAVE 


                List<int> arrr = ChecEveryItemInRow();

                if (arrr.Count > 0)
                {
                    //for (int i = 190; i < 200; i++)
                    //{
                    //    list[i].SetInGreen(); ;
                    //}

                    List<RectObj> list2 = new List<RectObj>();

                    // for (int i = 0; i < 200; i++)
                    //{
                 //   list.RemoveRange(190, 10);
                    // }
                    //for (int i = arrr.Count-1; i >= 0; i--)
                    //{
                        list.RemoveRange(arrr[0], arrr.Count);
                    // }

                      score += arrr.Count;
                    UpdateScore( score.ToString());
                    if (score > highScore)
                    {
                        highScore = score;
                    UpdateHighScore( highScore.ToString());
                    }


                    list2 = list.ToList();

                    for (int i = 0; i < arrr.Count; i++)
                    {
                        list2.Insert(0, new RectObj(this, Color.Red, "NEw Item_" + i.ToString()));
                    }

                    list = new List<RectObj>();

                    list = list2.ToList();

                    DrawAllRectangle();
                }

                currentShapeIndex = currentShapeIndexSuggest;
                UndrawDrawSuggest();
                currentShapeIndexSuggest= random.Next(0, 5);

                DrawSuggest();
                firstStart = true;
                initStart = 4;
               // InitShapes();

    
            }
        }


        private List<int>  ChecEveryItemInRow()
        {

            List<int> arr1 = new List<int>(); 
            for (int i = 0; i < 200; i += 10)
            {
                List<int> arr2 = new List<int>();
                for (int j = 0; j < 10; j++)
                {


                    if (list[i + j].TagName == "Taken")
                    {
                        arr2.Add(i+j);
                    }
                    else
                    {
                        arr2 = new List<int>();
                        break;
                    }
                }
                arr1.AddRange(arr2);

            }

            return arr1;
        }

        private bool IsEndofRow()
        {
            for (int i = 0; i < allShapes[0].GetLength(1); i++)
            {
                if (list[initStart+ allShapes[currentShapeIndex][currentRotationIndex, i] + noOfBlockInRow].TagName == "Taken")
                {
                    return true;
                }
            }
            return false;

        }

        private void EndGame()
        {
            for (int i = 0; i < allShapes[0].GetLength(1); i++)
            {
                if (list[initStart + allShapes[currentShapeIndex][currentRotationIndex, i] ].TagName == "Taken")
                {
                    Draw();
                    timer.Stop();
                    DisplayEnd("GAME END");
                    EnableStarAgaiButton();
                }
            }
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DrawSuggest();
            //  list[2].UndrawRectangle();
            timer.Start();
            button3.Enabled = true;
            button2.Enabled = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ChecEveryItemInRow();
            timer.Stop();

            button2.Enabled = true;
            button3.Enabled = false;
          //  MoveDown();

            //  CheckLeftFench();
            //if (!CheckRightFench())
            //{
            //    initStart += 1;
            //    UndrawDraw();

            //    InitShapes();
            //    Draw();
            //}

        }

        private void button4_Click(object sender, EventArgs e)
        {
            currentShapeIndexSuggest = random.Next(0, 5);
            currentShapeIndex = random.Next(0, 5);
            InitList();
            DrawAllRectangle();
            DrawAllRectangleSuggestion();
            txtDisplay.Text = "";

            score = 0;
            UpdateScore(score.ToString());
            button2.Enabled = true;
        }
    }


    public  class RectObj
    {
      

        private Control cntrl;
        public Rectangle Rect;
         //Pen pen;
        private Graphics graphics;
        Brush brush;

        public string TagName { get; set; } 
        public RectObj(Control _cntrl,Color color,string _tagName="Active" )
        {
            brush = new SolidBrush(color);
           
            cntrl = _cntrl;
            // graphics = cntrl.CreateGraphics();
          //  cntrl.Paint += Cntrl_Paint;
            TagName = _tagName;

        }

        private void Cntrl_Paint(object sender, PaintEventArgs e)
        {
          
            e.Graphics.FillRectangle(brush, Rect);
        }

        public void DrawRectangle(int x, int y, int width, int height)
        {
            Rect = new Rectangle(x, y, width, height);
        
            // graphics.DrawRectangle(pen, Rect);
     
            cntrl.CreateGraphics().FillRectangle(brush, Rect);
        }

        public void SetActive()
        {
            brush = new SolidBrush(Color.Blue);
            cntrl.CreateGraphics().FillRectangle(brush, Rect);

           // cntrl.Invalidate(Rect);
        }

        public void SetInActive()
        {
            
            brush = new SolidBrush(Color.Red);
            cntrl.CreateGraphics().FillRectangle(brush, Rect);
            // cntrl.Invalidate(Rect);
        }

        public void SetInGreen()
        {
            brush = new SolidBrush(Color.Green);
            cntrl.CreateGraphics().FillRectangle(brush, Rect);
            // cntrl.Invalidate(Rect);
        }
    }
}

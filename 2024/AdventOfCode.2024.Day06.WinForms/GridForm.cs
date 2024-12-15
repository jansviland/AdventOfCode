using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdventOfCode._2024.Day06.WinForms
{
    public partial class GridForm : Form
    {
        private char[,] _grid;
        private Timer _timer;

        public GridForm()
        {
            Width = 400;
            Height = 400;
            Text = "Animated Grid";

            // Enable double buffering to prevent flickering
            this.DoubleBuffered = true;

            _grid = new char[5, 7]
            {
                { '#', '#', '#', '#', '#', '#', '#' },
                { '#', '.', '.', '.', '.', '.', '#' },
                { '#', '.', '^', '.', '.', '.', '#' },
                { '#', '.', '.', '.', '.', '.', '#' },
                { '#', '#', '#', '#', '#', '#', '#' }
            };

            _timer = new Timer { Interval = 200 }; // Animation interval
            _timer.Tick += (s, e) =>
            {
                RandomizeGrid();
                Invalidate(); // Trigger the Paint event to redraw
            };

            _timer.Start();
            Paint += RenderGrid;
        }

        private void RenderGrid(object sender, PaintEventArgs e)
        {
            var cellSize = 50;
            var font = new Font("Consolas", 16);

            for (int y = 0; y < _grid.GetLength(0); y++)
            {
                for (int x = 0; x < _grid.GetLength(1); x++)
                {
                    char c = _grid[y, x];
                    Color color = (c == '^') ? Color.Green : Color.White;

                    // Draw the cell background
                    e.Graphics.FillRectangle(Brushes.Black, x * cellSize, y * cellSize, cellSize, cellSize);

                    // Draw the character
                    TextRenderer.DrawText(e.Graphics, c.ToString(), font,
                        new Point(x * cellSize + 15, y * cellSize + 10), color);
                }
            }
        }

        private void RandomizeGrid()
        {
            var random = new Random();
            for (int i = 0; i < 5; i++)
            {
                int x = random.Next(1, _grid.GetLength(1) - 1);
                int y = random.Next(1, _grid.GetLength(0) - 1);
                _grid[y, x] = (_grid[y, x] == '.') ? '*' : '.';
            }
        }
    }
}

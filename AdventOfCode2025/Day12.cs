using Microsoft.Z3;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2025
{
    public class Region
    {
        public int X => RegionShape.Length;
        public int Y => RegionShape[0].Vector.Length;
        public VectorV2[] RegionShape = new VectorV2[0];
        public List<(int, Pressent)> NeedToContaine = new List<(int, Pressent)>();
        public List<(int, Pressent[])> AllPosibleShapeInIt = new List<(int, Pressent[])>();

        public void Clrear()
        {
            NeedToContaine.Clear();
            AllPosibleShapeInIt.Clear();
        }

        public void BuildAllPosibleShapeInIt()
        {
            foreach (var shape in NeedToContaine)
            {
                AllPosibleShapeInIt.Add((shape.Item1, shape.Item2.GetAllPosition(RegionShape[0].Vector.Length, RegionShape.Length).ToArray()));
            }
        }
        public Pressent GetLayer(int layer)
        {
            foreach (var shape in AllPosibleShapeInIt)
            {
                foreach (var item in shape.Item2)
                {
                    if (layer == 0)
                    {
                        return item;
                    }
                    layer--;
                }
            }
            throw new Exception();
        }

        public Region(int x, int y)
        {
            RegionShape = new VectorV2[x];
            for (int i = 0; i < x; i++)
            {
                RegionShape[i] = new VectorV2(y);
            }
        }
    }
    public class Pressent
    {
        public VectorV2[] Shape = new VectorV2[0];
        public int Id = 0;
        public Pressent(int id)
        {
            Id = id;
        }
        public Pressent(VectorV2[] shape, int id)
        {
            Shape = shape;
            Id = id;
        }
        public int NumberOfSpaceUse()
        {
            var sum = 0;
            foreach (var item in Shape)
            {
                foreach (double v in item.Vector)
                {
                    sum += (int)v;
                }
            }
            return sum;
        }
        public bool EqualsV2(Pressent obj)
        {
            for (int i = 0; i < obj.Shape.Length; i++)
            {
                for (int j = 0; j < obj.Shape[i].Vector.Length; j++)
                {
                    if (this.Shape[i].Vector[j] != obj.Shape[i].Vector[j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public List<Pressent> GetAllVersionOfThisShape()
        {
            List<Pressent> returnValue = new List<Pressent>();
            for (int m = 0; m < 360; m += 90)
            {
                var currentShape = new VectorV2[this.Shape.Length];
                currentShape = Rotate(m);
                for (int n = 0; n < 2; n++)
                {
                    if (n == 0)
                    {
                        currentShape = Miror(currentShape);
                    }

                    if (returnValue.Count == 0 || returnValue.Where(o => o.EqualsV2(new Pressent(currentShape, Id))).Count() == 0)
                    {
                        returnValue.Add(new Pressent(currentShape, Id));
                    }

                }
            }
            return returnValue;
        }
        public List<Pressent> GetAllPosition(int lenght, int height)
        {
            List<Pressent> returnValue = new List<Pressent>();
            var AllPosition = GetAllVersionOfThisShape();

            foreach (Pressent currentShape in AllPosition)
            {
                for (int startPositionX = 0; startPositionX <= height - currentShape.Shape.Length; startPositionX++)
                {

                    for (int startPositionY = 0; startPositionY <= lenght - currentShape.Shape[0].Vector.Length; startPositionY++)
                    {
                        var newVector = new VectorV2[height];
                        for (int o = 0; o < height; o++)
                        {
                            newVector[o] = new VectorV2(lenght);
                            for (int p = 0; p < lenght; p++)
                            {
                                newVector[o].Vector[p] = 0;
                            }
                        }
                        for (int x = startPositionX; x < startPositionX + currentShape.Shape.Length; x++)
                        {
                            for (int y = startPositionY; y < startPositionY + currentShape.Shape[x - startPositionX].Vector.Length; y++)
                            {
                                newVector[x].Vector[y] = currentShape.Shape[x - startPositionX].Vector[y - startPositionY];
                            }
                        }

                        returnValue.Add(new Pressent(newVector, Id));
                    }
                }
            }
            return returnValue;
        }
        private VectorV2[] Miror(VectorV2[] shape)
        {
            var newVector = new VectorV2[shape.Length];
            for (int i = 0; i < shape.Length; i++)
            {
                newVector[i] = new VectorV2(shape[i].Vector.Length);
                for (int j = 0; j < shape[i].Vector.Length; j++)
                {
                    newVector[i].Vector[j] = shape[i].Vector[shape.Length - 1 - j];
                }
            }
            return newVector;
        }
        public VectorV2[] Rotate(int x)
        {
            var row = Shape.Length;
            var column = Shape[0].Vector.Length;
            var newVector = new VectorV2[row];
            for (int i = 0; i < Shape.Length; i++)
            {
                newVector[i] = new VectorV2(column);
                for (int j = 0; j < Shape[i].Vector.Length; j++)
                {
                    newVector[i].Vector[j] = x switch
                    {
                        0 => Shape[i].Vector[j],
                        90 => Shape[j].Vector[Shape.Length - 1 - i],
                        180 => Shape[Shape.Length - 1 - i].Vector[Shape.Length - 1 - j],
                        270 => Shape[Shape.Length - 1 - j].Vector[i],
                        _ => throw new Exception()
                    };

                }
            }
            return newVector;
        }
    }
    public class Day12
    {
        public Int128 SolveOne()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day12.txt");
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);

            int numberOfShape = 0;
            foreach (var item in inputs)
            {
                if (Regex.Match(item, @"^[0-9]:$").Success)
                {
                    numberOfShape++;
                }
            }

            Pressent[] allShape = new Pressent[numberOfShape];

            for (int i = 0; i < allShape.Length; i++)
            {
                allShape[i] = new Pressent(i);
                allShape[i].Shape = new VectorV2[3];
                for (int j = 0; j < allShape[i].Shape.Length; j++)
                {
                    allShape[i].Shape[j] = new VectorV2(3);
                }
            }
            var allRegion = new List<Region>();

            int currentShape = -1;
            int currentLineInShape = -1;
            foreach (var i in inputs)
            {
                if (Regex.Match(i, @"^[0-9]:$").Success)
                {
                    currentShape = int.Parse(Regex.Match(i, @"[0-9]:$").Value[..^1]);
                    currentLineInShape = 0;
                    continue;
                }
                if (currentShape != -1 && Regex.Match(i, @"^(#|\.)+$").Success)
                {
                    var line = Regex.Match(i, @"^(#|\.)+$").Value;
                    allShape[currentShape].Id = currentShape;
                    for (int j = 0; j < allShape[currentShape].Shape[currentLineInShape].Vector.Length; j++)
                    {
                        if (line[j].Equals('#'))
                        {
                            allShape[currentShape].Shape[currentLineInShape].Vector[j] = 1;
                        }
                    }
                    currentLineInShape++;
                }
                if (Regex.Match(i, @"^[0-9]+x[0-9]+\:").Success)
                {
                    var line = i.Split(":");
                    var xy = line[0].Split("x");
                    var x = int.Parse(xy[0]);
                    var y = int.Parse(xy[1]);
                    var newRegion = new Region(x, y);
                    allRegion.Add(newRegion);
                    var allShapeToUse = line[1].Trim().Split(" ");
                    for (int j = 0; j < allShapeToUse.Length; j++)
                    {
                        newRegion.NeedToContaine.Add((int.Parse(allShapeToUse[j]), allShape[j]));
                    }
                }
            }
            int sumCorrect = 0;
            foreach (var region in allRegion)
            {
                var size = region.X * region.Y;
                var numberOfElementToPutInside = region.NeedToContaine.Sum(t => t.Item1);
                if ((region.X / 3) * (region.Y / 3) >= numberOfElementToPutInside)
                {
                    sumCorrect++;
                    continue;
                }
                var sumOfStar = region.NeedToContaine.Sum(t => t.Item2.NumberOfSpaceUse() * t.Item1);
                if (sumOfStar > size)
                {
                    continue;
                }
                region.BuildAllPosibleShapeInIt();

                using (Context ctx = new Context())
                {
                    var solver = ctx.MkSolver();
                    int totalLayer = region.AllPosibleShapeInIt.Sum(s => s.Item2.Length);

                    IntExpr[] scalar = new IntExpr[totalLayer];
                    IntExpr[] sumScalrPerBlock = new IntExpr[region.NeedToContaine.Count];
                    for (int i = 0; i < sumScalrPerBlock.Length; i++)
                    {
                        sumScalrPerBlock[i] = ctx.MkInt(0);
                    }
                    for (int i = 0; i < totalLayer; i++)
                    {
                        scalar[i] = (IntExpr)ctx.MkIntConst("s_" + i);
                        sumScalrPerBlock[region.GetLayer(i).Id] = (IntExpr)ctx.MkAdd(sumScalrPerBlock[region.GetLayer(i).Id], scalar[i]);

                        solver.Assert(ctx.MkOr(ctx.MkEq(scalar[i], ctx.MkInt(0)), ctx.MkEq(scalar[i], ctx.MkInt(1))));

                    }

                    for (int x = 0; x < region.RegionShape.Length; x++)
                    {
                        for (int y = 0; y < region.RegionShape[x].Vector.Length; y++)
                        {
                            IntExpr collumnSum = ctx.MkInt(0);
                            for (int z = 0; z < totalLayer; z++)
                            {
                                collumnSum = (IntExpr)ctx.MkAdd(collumnSum, ctx.MkMul(scalar[z], ctx.MkInt((int)region.GetLayer(z).Shape[x].Vector[y])));
                            }

                            solver.Assert(ctx.MkOr(ctx.MkEq(collumnSum, ctx.MkInt(1)), ctx.MkEq(collumnSum, ctx.MkInt(0))));
                        }
                    }
                    for (int i = 0; i < sumScalrPerBlock.Length; i++)
                    {
                        solver.Assert(ctx.MkEq(sumScalrPerBlock[i], ctx.MkInt(region.NeedToContaine[i].Item1)));
                    }

                    Status result = solver.Check();
                    if (result == Status.SATISFIABLE)
                    {
                        sumCorrect++;
                    }
                    else if (result == Status.UNSATISFIABLE)
                    {
                        var core = solver.UnsatCore;
                        foreach (var item in core)
                        {
                            Console.WriteLine(item);
                        }
                        //Console.WriteLine("No solution found.");
                    }
                    else
                    {

                    }
                }
                region.Clrear();
            }
            return sumCorrect;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OBJMedium;

namespace PlaneBorder
{
    class Program
    {
        static void Main(string[] args)
        {
            void ShowInfo()
            {
                AdvConsole.WriteLine(ConsoleColor.Cyan,
                    "PlaneBorder\n" +
                    "by Zach Combs\n" +
                    "\n" +
                    "<source OBJ> <dest OBJ> <grid x> <gird y>\n" +
                    "     Maps textures for a VVVVVV Circuit style track.\n" +
                    "     source OBJ      The source OBJ file\n" +
                    "     dest OBj        The destination OBJ file\n" +
                    "     corner width    The width of a corner\n" +
                    "     corner height   The height of a corner");
            }
            void WriteError(string s)
            {
                AdvConsole.WriteLine(ConsoleColor.Red, "ERROR: " + s);
            }
            Rect2d[] CalculateVerticesOfRect(float width, float height, float cornerwidth, float cornerheight)
            {
                //coords[0] = topleft corner
                //coords[1] = topright corner
                //coords[2] = bottomleft corner
                //coords[3] = bottomright corner
                //coords[4] = left side
                //coords[5] = right side
                //coords[6] = bottom side
                //coords[7] = top side
                //coords[8] = center
                Rect2d[] coords = new Rect2d[9];

                Rect2d corner_topleft = new Rect2d();
                Rect2d corner_topright = new Rect2d();
                Rect2d corner_bottomleft = new Rect2d();
                Rect2d corner_bottomright = new Rect2d();
                Rect2d side_left = new Rect2d();
                Rect2d side_right = new Rect2d();
                Rect2d side_bottom = new Rect2d();
                Rect2d side_top = new Rect2d();
                Rect2d center = new Rect2d();

                ObjVector2 in_topleft = new ObjVector2();
                ObjVector2 in_bottomright = new ObjVector2();
                if (width < (cornerwidth * 2))
                {
                    in_topleft.X = width / 2;
                }
                else
                {
                    in_topleft.X = cornerwidth;
                }
                if (height < (cornerheight * 2))
                {
                    in_topleft.Y = height / 2;
                }
                else
                {
                    in_topleft.Y = cornerheight;
                }
                if (width < (cornerwidth * 2))
                {
                    in_bottomright.X = width / 2;
                }
                else
                {
                    in_bottomright.X = width-cornerwidth;
                }
                if (height < (cornerheight * 2))
                {
                    in_bottomright.Y = height / 2;
                }
                else
                {
                    in_bottomright.Y = height-cornerheight;
                }

                corner_topleft = new Rect2d(
                    new ObjVector2(0, 0),
                    new ObjVector2(in_topleft.X, in_topleft.Y));
                corner_topright = new Rect2d(
                    new ObjVector2(in_bottomright.X, 0),
                    new ObjVector2(width, in_topleft.Y));
                corner_bottomleft = new Rect2d(
                    new ObjVector2(0, in_bottomright.Y),
                    new ObjVector2(in_topleft.X, height));
                corner_bottomright = new Rect2d(
                    new ObjVector2(in_bottomright.X, in_bottomright.Y),
                    new ObjVector2(width, height));

                side_left = new Rect2d(
                    new ObjVector2(0, in_topleft.Y),
                    new ObjVector2(in_topleft.X, in_bottomright.Y));
                side_right = new Rect2d(
                    new ObjVector2(in_bottomright.X, in_topleft.Y),
                    new ObjVector2(width, in_bottomright.Y));
                side_bottom = new Rect2d(
                    new ObjVector2(in_topleft.X, in_bottomright.Y),
                    new ObjVector2(in_bottomright.X, height));
                side_top = new Rect2d(
                    new ObjVector2(in_topleft.X, 0),
                    new ObjVector2(in_bottomright.X, in_topleft.Y));

                center = new Rect2d(
                    new ObjVector2(in_topleft.X, in_topleft.Y),
                    new ObjVector2(in_bottomright.X, in_bottomright.Y));

                coords[0] = corner_topleft;
                coords[1] = corner_topright;
                coords[2] = corner_bottomleft;
                coords[3] = corner_bottomright;
                coords[4] = side_left;
                coords[5] = side_right;
                coords[6] = side_bottom;
                coords[7] = side_top;
                coords[8] = center;
                return coords;
            }
            Rect2d[] CalculateTexCoordsOfRect(float width, float height, float cornerwidth, float cornerheight)
            {
                //coords[0] = topleft corner
                //coords[1] = topright corner
                //coords[2] = bottomleft corner
                //coords[3] = bottomright corner
                //coords[4] = left side
                //coords[5] = right side
                //coords[6] = bottom side
                //coords[7] = top side
                //coords[8] = center
                Rect2d[] coords = new Rect2d[9];

                coords[0] = new Rect2d(
                    new ObjVector2(0.125F, 0F),
                    new ObjVector2(0.25F, 0.5F));
                coords[1] = new Rect2d(
                    new ObjVector2(0.25F, 0F),
                    new ObjVector2(0.375F, 0.5F));
                coords[2] = new Rect2d(
                    new ObjVector2(0.125F, 0.5F),
                    new ObjVector2(0.25F, 1F));
                coords[3] = new Rect2d(
                    new ObjVector2(0.25F, 0.5F),
                    new ObjVector2(0.375F, 1F));

                float in_width;
                float in_height;
                if (width < (cornerwidth * 2))
                {
                    in_width = 0;
                }
                else
                {
                    in_width = width - (cornerwidth * 2);
                }
                if (height < (cornerheight * 2))
                {
                    in_height = 0;
                }
                else
                {
                    in_height = height - (cornerheight * 2);
                }
                Rect2d side_left = new Rect2d(
                    new ObjVector2(0.375F, 0F),
                    new ObjVector2(0.5F, in_height / (cornerheight * 2)));
                Rect2d side_right = new Rect2d(
                    new ObjVector2(0.5F, 0F),
                    new ObjVector2(0.625F, in_height / (cornerheight * 2)));
                Rect2d side_bottom = new Rect2d(
                    new ObjVector2(0.625F, 0F),
                    new ObjVector2(0.75F, in_width / (cornerwidth * 2)));//This will be rotated counter-clockwise
                Rect2d side_top = new Rect2d(
                    new ObjVector2(0.75F, 0F),
                    new ObjVector2(0.875F, in_width / (cornerwidth * 2)));//This will be rotated counter-clockwise
                Rect2d center = new Rect2d(
                    new ObjVector2(0F, 0F),
                    new ObjVector2(in_width / (cornerwidth * 2), in_height / (cornerheight * 2)));



                coords[4] = side_left;
                coords[5] = side_right;
                coords[6] = side_bottom;
                coords[7] = side_top;
                coords[8] = center;
                return coords;
            }
            bool IsARectangle(ObjVector2 v1, ObjVector2 v2, ObjVector2 v3, ObjVector2 v4, out ObjVector2[] outpoints, out int[] oldindexes, out bool isclockwise)
            {
                //Returns whether or not v1, v2, v3, v4 make a rectangle
                //The vertices need to be in the order of building a rectangle
                //If v1, v2, v3, v4 make a rectangle, it makes an array of the vertices where the top-left vertex is first
                //The order of the vertices in the array depends on whether or not the rectangle the built clockwise or counter-clockwise
                int topleft = -1;
                float mincoordx = Math.Min(Math.Min(v1.X, v2.X), Math.Min(v3.X, v4.X));
                float mincoordy = Math.Min(Math.Min(v1.Y, v2.Y), Math.Min(v3.Y, v4.Y));
                if (v1.X == mincoordx & v1.Y == mincoordy) topleft = 0;
                if (v2.X == mincoordx & v2.Y == mincoordy) topleft = 1;
                if (v3.X == mincoordx & v3.Y == mincoordy) topleft = 2;
                if (v4.X == mincoordx & v4.Y == mincoordy) topleft = 3;
                if (topleft == -1)
                {
                    outpoints = null;
                    oldindexes = null;
                    isclockwise = false;
                    return false;
                }
                else
                {
                    ObjVector2 newv1 = null;
                    ObjVector2 newv2 = null;
                    ObjVector2 newv3 = null;
                    ObjVector2 newv4 = null;
                    int oldind1 = -1;
                    int oldind2 = -1;
                    int oldind3 = -1;
                    int oldind4 = -1;
                    if (topleft == 0)
                    {
                        newv1 = new ObjVector2(v1.X, v1.Y);
                        newv2 = new ObjVector2(v2.X, v2.Y);
                        newv3 = new ObjVector2(v3.X, v3.Y);
                        newv4 = new ObjVector2(v4.X, v4.Y);
                        oldind1 = 0;
                        oldind2 = 1;
                        oldind3 = 2;
                        oldind4 = 3;
                    }
                    else if (topleft == 1)
                    {
                        newv1 = new ObjVector2(v2.X, v2.Y);
                        newv2 = new ObjVector2(v3.X, v3.Y);
                        newv3 = new ObjVector2(v4.X, v4.Y);
                        newv4 = new ObjVector2(v1.X, v1.Y);
                        oldind1 = 1;
                        oldind2 = 2;
                        oldind3 = 3;
                        oldind4 = 0;
                    }
                    else if (topleft == 2)
                    {
                        newv1 = new ObjVector2(v3.X, v3.Y);
                        newv2 = new ObjVector2(v4.X, v4.Y);
                        newv3 = new ObjVector2(v1.X, v1.Y);
                        newv4 = new ObjVector2(v2.X, v2.Y);
                        oldind1 = 2;
                        oldind2 = 3;
                        oldind3 = 0;
                        oldind4 = 1;
                    }
                    else if (topleft == 3)
                    {
                        newv1 = new ObjVector2(v4.X, v4.Y);
                        newv2 = new ObjVector2(v1.X, v1.Y);
                        newv3 = new ObjVector2(v2.X, v2.Y);
                        newv4 = new ObjVector2(v3.X, v3.Y);
                        oldind1 = 3;
                        oldind2 = 0;
                        oldind3 = 1;
                        oldind4 = 2;
                    }
                    bool isrectccw = (newv1.X == newv2.X & newv2.Y == newv3.Y & newv3.X == newv4.X & newv4.Y == newv1.Y);
                    bool isrectcw = (newv1.Y == newv2.Y & newv2.X == newv3.X & newv3.Y == newv4.Y & newv4.X == newv1.X);
                    if (isrectccw | isrectcw)
                    {

                        outpoints = new ObjVector2[4];
                        outpoints[0] = newv1;
                        outpoints[1] = newv2;
                        outpoints[2] = newv3;
                        outpoints[3] = newv4;
                        oldindexes = new int[4];
                        oldindexes[0] = oldind1;
                        oldindexes[1] = oldind2;
                        oldindexes[2] = oldind3;
                        oldindexes[3] = oldind4;
                        isclockwise = isrectcw;
                        return true;
                    }
                    else
                    {
                        outpoints = null;
                        oldindexes = null;
                        isclockwise = false;
                        return false;
                    }
                }
            }
            double PointDirection(ObjVector2 pnt1, ObjVector2 pnt2)
            {
                double xxx = pnt2.X - pnt1.X;
                double yyy = pnt2.Y - pnt1.Y;
                if (xxx == 0)
                {
                    if (yyy < 0)
                    {
                        return Math.PI * 1.5D;
                    }
                    else
                    {
                        return Math.PI * 0.5D;
                    }
                }
                else if (xxx < 0)
                {
                    return Math.Atan(yyy / xxx) + Math.PI;
                }
                else
                {
                    return Math.Atan(yyy / xxx);
                }
            }

            const int reqargs = 4;
            if (args == null)
            {
                ShowInfo();
                return;
            }
            if (args.Length == 0)
            {
                ShowInfo();
                return;
            }

            if (args.Length < reqargs)
            {
                WriteError(
                    "You must specify source obj, dest obj, corner width, and corner height"
                    );
                return;
            }

            string file_source = args[0];
            string file_dest = args[1];
            float corner_width = 0f;
            float corner_height = 0f;
            if (!File.Exists(file_source))
            {
                WriteError(String.Format(
                    "\"{0}\" does not exist"
                    , file_source));
                return;
            }
            if (File.Exists(file_dest))
            {
                WriteError(String.Format(
                    "\"{0}\" already exists"
                    , file_dest));
                return;
            }
            if (!float.TryParse(args[2], out corner_width))
            {
                WriteError(String.Format(
                    "\"{0}\" is not valid"
                    , args[2]));
                return;
            }
            if (!float.TryParse(args[3], out corner_height))
            {
                WriteError(String.Format(
                    "\"{0}\" is not valid"
                    , args[3]));
                return;
            }
            
            try
            {
                Obj obj = new Obj();
                obj.Load(file_source, ObjLoadMode.IgnoreGroups, false);
                ObjGroup group = obj.Groups.Groups[0];

                ObjGroup newgroup = new ObjGroup();
                int AddVertex(ObjVector3 vertex)
                {
                    int index = -1;
                    int n = 0;
                    while (n < newgroup.Vertexes.Count & index == -1)
                    {
                        ObjVector3 vector = newgroup.Vertexes.Item(n);
                        if (
                            vertex.X == vector.X &
                            vertex.Y == vector.Y &
                            vertex.Z == vector.Z)
                        {
                            index = n;
                        }
                        n += 1;
                    }
                    if (index == -1)
                    {
                        index = newgroup.Vertexes.Count;
                        newgroup.Vertexes.Add(vertex);
                    }
                    return index;
                }
                void AddRectangle(
                    ObjVector3 topleftpos,
                    int coordplane,
                    Rect2d[] Vertices, Rect2d[] TexCoords)
                {
                    //coordplane==0: +X+Z plane
                    //coordplane==1: -X+Z plane
                    //coordplane==2: +X-Y plane
                    //coordplane==3: -X-Y plane
                    //coordplane==4: +Z-Y plane
                    //coordplane==5: -Z-Y plane

                    ObjVector3 DeterminePosition(float vx, float vy)
                    {
                        ObjVector3 vector = new ObjVector3(
                            topleftpos.X,
                            topleftpos.Y,
                            topleftpos.Z);

                        if (coordplane == 0)
                        {
                            vector.X += vx;
                            vector.Z += vy;
                        }
                        else if (coordplane == 1)
                        {
                            vector.X += -vx;
                            vector.Z += vy;
                        }
                        else if (coordplane == 2)
                        {
                            vector.X += vx;
                            vector.Y += -vy;
                        }
                        else if (coordplane == 3)
                        {
                            vector.X += -vx;
                            vector.Y += -vy;
                        }
                        else if (coordplane == 4)
                        {
                            vector.Z += vx;
                            vector.Y += -vy;
                        }
                        else if (coordplane == 5)
                        {
                            vector.Z += -vx;
                            vector.Y += -vy;
                        }

                        return vector;
                    }
                    ObjFacePoint NewFacePoint(int v, int vt)
                    {
                        ObjFacePoint facepoint = new ObjFacePoint();
                        facepoint.Vertex_Index = v;
                        facepoint.TexCoord_Index = vt;
                        return facepoint;
                    }
                    for (int n = 0; n < Math.Min(
                        Vertices.Length, TexCoords.Length
                        ); n += 1)
                    {
                        Rect2d rect_v = Vertices[n];
                        Rect2d rect_tc = TexCoords[n];

                        if (
                            (rect_v.Pos_TopLeft.X != rect_v.Pos_BottomRight.X) &
                            (rect_v.Pos_TopLeft.Y != rect_v.Pos_BottomRight.Y)
                            )
                        {
                            ObjVector2 newtexcoord_tl = new ObjVector2(
                            rect_tc.Pos_TopLeft.X,
                            rect_tc.Pos_TopLeft.Y);
                            ObjVector2 newtexcoord_tr = new ObjVector2(
                                rect_tc.Pos_BottomRight.X,
                                rect_tc.Pos_TopLeft.Y);
                            ObjVector2 newtexcoord_bl = new ObjVector2(
                                rect_tc.Pos_TopLeft.X,
                                rect_tc.Pos_BottomRight.Y);
                            ObjVector2 newtexcoord_br = new ObjVector2(
                                rect_tc.Pos_BottomRight.X,
                                rect_tc.Pos_BottomRight.Y);
                            if (n == 6 | n == 7)
                            {
                                //These are the top/bottom rectangles
                                newtexcoord_tl.X = rect_tc.Pos_BottomRight.X;
                                newtexcoord_tl.Y = rect_tc.Pos_TopLeft.Y;
                                newtexcoord_tr.X = rect_tc.Pos_BottomRight.X;
                                newtexcoord_tr.Y = rect_tc.Pos_BottomRight.Y;
                                newtexcoord_bl.X = rect_tc.Pos_TopLeft.X;
                                newtexcoord_bl.Y = rect_tc.Pos_TopLeft.Y;
                                newtexcoord_br.X = rect_tc.Pos_TopLeft.X;
                                newtexcoord_br.Y = rect_tc.Pos_BottomRight.Y;
                            }

                            ObjVector3 newvertex_tl = DeterminePosition(
                                rect_v.Pos_TopLeft.X,
                                rect_v.Pos_TopLeft.Y);
                            ObjVector3 newvertex_tr = DeterminePosition(
                                rect_v.Pos_BottomRight.X,
                                rect_v.Pos_TopLeft.Y);
                            ObjVector3 newvertex_bl = DeterminePosition(
                                rect_v.Pos_TopLeft.X,
                                rect_v.Pos_BottomRight.Y);
                            ObjVector3 newvertex_br = DeterminePosition(
                                rect_v.Pos_BottomRight.X,
                                rect_v.Pos_BottomRight.Y);

                            int v_index_tl = AddVertex(newvertex_tl);
                            int v_index_tr = AddVertex(newvertex_tr);
                            int v_index_bl = AddVertex(newvertex_bl);
                            int v_index_br = AddVertex(newvertex_br);
                            int tc_index_tl = newgroup.TexCoords.Count;
                            newgroup.TexCoords.Add(newtexcoord_tl);
                            int tc_index_tr = newgroup.TexCoords.Count;
                            newgroup.TexCoords.Add(newtexcoord_tr);
                            int tc_index_bl = newgroup.TexCoords.Count;
                            newgroup.TexCoords.Add(newtexcoord_bl);
                            int tc_index_br = newgroup.TexCoords.Count;
                            newgroup.TexCoords.Add(newtexcoord_br);

                            ObjFace newface = new ObjFace();
                            newface.UsesNormals = false;
                            newface.UsesTexCoords = true;
                            newface.Points.Add(NewFacePoint(
                                v_index_tl, tc_index_tl
                                ));
                            newface.Points.Add(NewFacePoint(
                                v_index_bl, tc_index_bl
                                ));
                            newface.Points.Add(NewFacePoint(
                                v_index_br, tc_index_br
                                ));
                            newface.Points.Add(NewFacePoint(
                                v_index_tr, tc_index_tr
                                ));
                            newgroup.Faces.Add(newface);
                        }
                    }
                }

                #region Create rectangles
                //Create rectangles
                //Create vertices and textures
                AdvConsole.WriteLine(ConsoleColor.Green,
                    "Creating rectangles"
                    );
                for (int n = 0; n < group.Faces.Count; n += 1)
                {
                    ObjFace face = group.Faces.Item(n);
                    ObjFacePointCollection points = face.Points;
                    List<ObjVector3> vertexes = new List<ObjVector3>();
                    for (int m = 0; m < points.Count; m += 1)
                    {
                        int prev = m - 1; if (prev < 0) prev = points.Count - 1;
                        int next = m + 1; if (next >= points.Count) next = 0;
                        int index = points.Item(m).Vertex_Index;
                        int index_p = points.Item(prev).Vertex_Index;
                        int index_n = points.Item(next).Vertex_Index;
                        ObjVector3 vertex = group.Vertexes.Item(index);
                        ObjVector3 vertex_p = group.Vertexes.Item(index_p);
                        ObjVector3 vertex_n = group.Vertexes.Item(index_n);
                        bool xsame = (vertex.X == vertex_p.X & vertex.X == vertex_n.X);
                        bool ysame = (vertex.Y == vertex_p.Y & vertex.Y == vertex_n.Y);
                        bool zsame = (vertex.Z == vertex_p.Z & vertex.Z == vertex_n.Z);
                        if (!((xsame & zsame) |
                            (xsame & ysame) |
                            (zsame & ysame)))
                        {
                            vertexes.Add(vertex);
                        }
                    }
                    if (vertexes.Count == 4)
                    {
                        ObjVector3 v1 = vertexes[0];
                        ObjVector3 v2 = vertexes[1];
                        ObjVector3 v3 = vertexes[2];
                        ObjVector3 v4 = vertexes[3];
                        bool isonx = (v1.X == v2.X & v2.X == v3.X & v3.X == v4.X);
                        bool isony = (v1.Y == v2.Y & v2.Y == v3.Y & v3.Y == v4.Y);
                        bool isonz = (v1.Z == v2.Z & v2.Z == v3.Z & v3.Z == v4.Z);
                        if (isonx | isony | isonz)
                        {
                            //coordplane==0: +X+Z plane
                            //coordplane==1: -X+Z plane
                            //coordplane==2: +X-Y plane
                            //coordplane==3: -X-Y plane
                            //coordplane==4: +Z-Y plane
                            //coordplane==5: -Z-Y plane

                            int coordplane;
                            bool isclockwise;
                            ObjVector2[] rectpoints;
                            int[] oldindexes;
                            int axis = -1; //0=X; 1=Y; 2=Z;
                            if (isonx) axis = 0;
                            if (isony) axis = 1;
                            if (isonz) axis = 2;
                            ObjVector2 v1_2 = null;
                            ObjVector2 v2_2 = null;
                            ObjVector2 v3_2 = null;
                            ObjVector2 v4_2 = null;
                            if (axis == 0)
                            {
                                v1_2 = new ObjVector2(
                                    v1.Z, -v1.Y
                                    );
                                v2_2 = new ObjVector2(
                                    v2.Z, -v2.Y
                                    );
                                v3_2 = new ObjVector2(
                                    v3.Z, -v3.Y
                                    );
                                v4_2 = new ObjVector2(
                                    v4.Z, -v4.Y
                                    );
                            }
                            if (axis == 1)
                            {
                                v1_2 = new ObjVector2(
                                    v1.X, v1.Z
                                    );
                                v2_2 = new ObjVector2(
                                    v2.X, v2.Z
                                    );
                                v3_2 = new ObjVector2(
                                    v3.X, v3.Z
                                    );
                                v4_2 = new ObjVector2(
                                    v4.X, v4.Z
                                    );
                            }
                            if (axis == 2)
                            {
                                v1_2 = new ObjVector2(
                                    v1.X, -v1.Y
                                    );
                                v2_2 = new ObjVector2(
                                    v2.X, -v2.Y
                                    );
                                v3_2 = new ObjVector2(
                                    v3.X, -v3.Y
                                    );
                                v4_2 = new ObjVector2(
                                    v4.X, -v4.Y
                                    );
                            }
                            if (IsARectangle(v1_2, v2_2, v3_2, v4_2, out rectpoints, out oldindexes, out isclockwise))
                            {
                                float width;
                                float height;
                                int topleftindex;
                                if (isclockwise)
                                {
                                    ObjVector2 tl = rectpoints[1];
                                    ObjVector2 br = rectpoints[3];
                                    topleftindex = oldindexes[1];
                                    width = -(br.X - tl.X);
                                    height = br.Y - tl.Y;
                                }
                                else
                                {
                                    ObjVector2 tl = rectpoints[0];
                                    ObjVector2 br = rectpoints[2];
                                    topleftindex = oldindexes[0];
                                    width = br.X - tl.X;
                                    height = br.Y - tl.Y;
                                }
                                Rect2d[] rects_v = CalculateVerticesOfRect(
                                    width, height, corner_width, corner_height
                                    );
                                Rect2d[] rects_vt = CalculateTexCoordsOfRect(
                                    width, height, corner_width, corner_height
                                    );
                                if (axis == 1 & !isclockwise) coordplane = 0;
                                else if (axis == 1 & isclockwise) coordplane = 1;
                                else if (axis == 2 & !isclockwise) coordplane = 2;
                                else if (axis == 2 & isclockwise) coordplane = 3;
                                else if (axis == 0 & !isclockwise) coordplane = 4;
                                else coordplane = 5;
                                AddRectangle(vertexes[topleftindex], coordplane, rects_v, rects_vt);
                            }


                        }
                    }
                }
                #endregion

                #region Finalize faces
                AdvConsole.Write(ConsoleColor.Green,
                    "Finalizing faces "
                    );
                int cursorleft = Console.CursorLeft;
                int cursortop = Console.CursorTop;
                for (int n = 0; n <= newgroup.Faces.Count; n += 1)
                {
                    Console.CursorLeft = cursorleft;
                    Console.CursorTop = cursortop;
                    AdvConsole.WriteLine(ConsoleColor.Green,
                        "({0}/{1})"
                        , n, newgroup.Faces.Count);
                    if (n < newgroup.Faces.Count)
                    {
                        int index = newgroup.Faces.Count - 1 - n;
                        ObjFace face = newgroup.Faces.Item(index);
                        ObjFacePointCollection newpoints = new ObjFacePointCollection();
                        for (int m = 0; m < 4; m += 1)
                        {
                            int next = m + 1; if (next >= 4) next = 0;
                            ObjFacePoint pnt = face.Points.Item(m);
                            ObjFacePoint pnt_next = face.Points.Item(next);
                            int ind = pnt.Vertex_Index;
                            int ind_next = pnt_next.Vertex_Index;
                            int ind_t = pnt.TexCoord_Index;
                            int ind_t_next = pnt_next.TexCoord_Index;
                            ObjVector3 v = newgroup.Vertexes.Item(ind);
                            ObjVector3 v_next = newgroup.Vertexes.Item(ind_next);
                            ObjVector2 vt = newgroup.TexCoords.Item(ind_t);
                            ObjVector2 vt_next = newgroup.TexCoords.Item(ind_t_next);
                            double distanceapart = Math.Sqrt(
                                Math.Pow(v_next.X - v.X, 2) +
                                Math.Pow(v_next.Y - v.Y, 2) +
                                Math.Pow(v_next.Z - v.Z, 2)
                                );
                            double t_distanceapart = Math.Sqrt(
                                Math.Pow(vt_next.X - vt.X, 2) +
                                Math.Pow(vt_next.Y - vt.Y, 2)
                                );
                            double t_direction = PointDirection(
                                vt, vt_next
                                );

                            int axis = -1;//0=X different; 1=Y different; 2=Z different
                            if (v.Y == v_next.Y & v.Z == v_next.Z) axis = 0;
                            if (v.X == v_next.X & v.Z == v_next.Z) axis = 1;
                            if (v.X == v_next.X & v.Y == v_next.Y) axis = 2;

                            ObjFacePoint point_first = new ObjFacePoint();
                            point_first.Vertex_Index = ind;
                            point_first.TexCoord_Index = ind_t;
                            newpoints.Add(point_first);

                            Dictionary<int, float> newinpoints = new Dictionary<int, float>();//int=vertex index
                            float val;
                            float val_next;
                            if (axis == 0)
                            {
                                val = v.X;
                                val_next = v_next.X;
                            }
                            else if (axis == 1)
                            {
                                val = v.Y;
                                val_next = v_next.Y;
                            }
                            else
                            {
                                val = v.Z;
                                val_next = v_next.Z;
                            }
                            for (int o = 0; o < newgroup.Vertexes.Count; o += 1)
                            {
                                if (o != ind & o != ind_next)
                                {
                                    ObjVector3 vector = newgroup.Vertexes.Item(o);
                                    if (
                                        (axis == 0 & vector.Y == v.Y & vector.Z == v.Z &
                                            vector.X > Math.Min(val, val_next) &
                                            vector.X < Math.Max(val, val_next)) |
                                        (axis == 1 & vector.X == v.X & vector.Z == v.Z &
                                            vector.Y > Math.Min(val, val_next) &
                                            vector.Y < Math.Max(val, val_next)) |
                                        (axis == 2 & vector.X == v.X & vector.Y == v.Y &
                                            vector.Z > Math.Min(val, val_next) &
                                            vector.Z < Math.Max(val, val_next))
                                        )
                                    {
                                        float val_in;
                                        if (axis == 0) val_in = vector.X;
                                        else if (axis == 1) val_in = vector.Y;
                                        else val_in = vector.Z;
                                        newinpoints.Add(o, val_in);
                                    }
                                }
                            }
                            int[] nip_keys = newinpoints.Keys.ToArray();
                            float[] nip_values = newinpoints.Values.ToArray();
                            Array.Sort(nip_values, nip_keys);
                            if (val > val_next)
                            {
                                Array.Reverse(nip_keys);
                                Array.Reverse(nip_values);
                            }
                            for (int o = 0; o < nip_keys.Length; o += 1)
                            {
                                int nip_key = nip_keys[o];
                                ObjVector3 inpnt = newgroup.Vertexes.Item(nip_key);
                                double fromstrt2pnt = Math.Sqrt(
                                    Math.Pow(inpnt.X - v.X, 2) +
                                    Math.Pow(inpnt.Y - v.Y, 2) +
                                    Math.Pow(inpnt.Z - v.Z, 2)
                                    );
                                double xx = vt.X + Math.Cos(t_direction) * (fromstrt2pnt / distanceapart) * t_distanceapart;
                                double yy = vt.Y + Math.Sin(t_direction) * (fromstrt2pnt / distanceapart) * t_distanceapart;
                                int tind = newgroup.TexCoords.Count; newgroup.TexCoords.Add(
                                    new ObjVector2((float)xx, (float)yy));

                                ObjFacePoint point_in = new ObjFacePoint();
                                point_in.Vertex_Index = nip_key;
                                point_in.TexCoord_Index = tind;
                                newpoints.Add(point_in);
                            }
                        }
                        face.Points = newpoints;
                    }
                }
                #endregion

                #region Save OBJ
                AdvConsole.WriteLine(ConsoleColor.Green,
                    "Saving OBJ"
                    );
                string DetermineMTLSaveName()
                {
                    string filewithoutext = Path.GetDirectoryName(
                        Path.GetFullPath(file_source)) +
                        Path.GetFileNameWithoutExtension(
                            file_source);
                    string mtlfilename = filewithoutext + ".mtl";
                    int i = 0;
                    while (File.Exists(mtlfilename))
                    {
                        mtlfilename = filewithoutext + i.ToString() + ".mtl";
                        i += 1;
                    }
                    return mtlfilename;
                }
                Obj newobj = new Obj();
                newobj.Groups.Add("newgroup", newgroup);
                newobj.Save(file_dest, DetermineMTLSaveName());
                #endregion
            }
            catch (Exception ex)
            {
                WriteError("Could not complete program\n" +
                    ex.Message);
            }
        }
    }
}

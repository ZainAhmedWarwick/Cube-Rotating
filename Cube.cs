using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;


namespace Cube_Rotating
{
    internal class Cube
    {
        public Vector3 vertex;
        public Vector2 cubevertex;
        public List<Vector2> vertices = new List<Vector2>();
        public List<Vector3> vertices3D = new List<Vector3>();
        public Vector3 origin = new Vector3(0, 0, 0);
        public Texture2D sprite;
        public Texture2D sprite2;
        public SpriteFont font;
        public Vector2 screencenter;
        public float rotationangle = 0f;
        private List<(int, int)> edges = new List<(int, int)>();


       public void Edgesdefinition()
        {
            edges.Clear();
            edges.Add((0, 1));
            edges.Add((0, 2));
            edges.Add((0, 4));
            edges.Add((1, 3));
            edges.Add((1, 5));
            edges.Add((2, 3));
            edges.Add((2, 6));
            edges.Add((3, 7));
            edges.Add((4, 5));
            edges.Add((4, 6));
            edges.Add((5, 7));
            edges.Add((6, 7));
        }

        public List<Vector2> DefineVertices(int size)
        {
            vertices.Clear(); // clear old vertices before re-adding
            vertices3D.Clear();

            // Define vertices to match your edge definitions exactly
            vertices3D.Add(new Vector3(-size, -size, -size)); // 0
            vertices3D.Add(new Vector3(size, -size, -size));  // 1  
            vertices3D.Add(new Vector3(-size, size, -size));  // 2
            vertices3D.Add(new Vector3(size, size, -size));   // 3
            vertices3D.Add(new Vector3(-size, -size, size));  // 4
            vertices3D.Add(new Vector3(size, -size, size));   // 5
            vertices3D.Add(new Vector3(-size, size, size));   // 6
            vertices3D.Add(new Vector3(size, size, size));    // 7

            return vertices;
        }

        public Vector2 Project3Dto2D(Vector3 v)
        {
            float distance = 200f; // Camera distance
            float scale = distance / (distance + v.Z);
            return new Vector2(v.X * scale, v.Y * scale);
        }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Cube2");
            sprite2 = content.Load<Texture2D>("Cube1");
            font = content.Load<SpriteFont>("font");
        }


        public void Update(GameTime gameTime)
        {
            rotationangle += 0.01f;
            vertices.Clear();

            foreach (var v in vertices3D)
            {
                var rotated = RotateVertexZ(RotateVertexY(RotateVertexX(v, rotationangle), rotationangle), rotationangle); 
                vertices.Add(Project3Dto2D(rotated));
            }

        }

        public Vector3 RotateVertexY(Vector3 vertex, float angle)
        {
            float cosA = MathF.Cos(angle);
            float sinA = MathF.Sin(angle);

            float x = vertex.X * cosA - vertex.Z * sinA;
            float z = vertex.X * sinA + vertex.Z * cosA;

            return new Vector3(x, vertex.Y, z);
        }

        public Vector3 RotateVertexX(Vector3 vertex, float angle)
        {
            float cosA = MathF.Cos(angle);
            float sinA = MathF.Sin(angle);

            float y = vertex.Y * cosA - vertex.Z * sinA;
            float z = vertex.Y * sinA + vertex.Z * cosA;

            return new Vector3(vertex.X, y, z);
        }

        public Vector3 RotateVertexZ(Vector3 vertex, float angle)
        {
            float cosA = MathF.Cos(angle);
            float sinA = MathF.Sin(angle);

            float x = vertex.X * cosA - vertex.Y * sinA;
            float y = vertex.X * sinA + vertex.Y * cosA;

            return new Vector3(x, y, vertex.Z);
        }

        public void DrawCube(SpriteBatch spriteBatch)
        {
            float scale = 75f;
            for (int i = 0; i < vertices.Count; i++)
            {
                Vector2 cubeverticesRaw = vertices[i];
                Vector2 cubeverticies = cubeverticesRaw * scale + screencenter; 
                spriteBatch.Draw(sprite, cubeverticies, Color.White);

                string text = cubeverticies.ToString();
                Vector2 offset = new Vector2(50, 50);
                //spriteBatch.DrawString(font, text, cubeverticies + offset, Color.White);


            }

        }



    }
}


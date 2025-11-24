using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTK_winforms_z02
{
    public class SursaDeLumina
    {
        // Proprietati standard
        public LightName LightName { get; private set; }
        public bool IsEnabled { get; private set; }
        public Vector4 Position { get; set; }
        public Vector4 Ambient { get; set; }
        public Vector4 Diffuse { get; set; }
        public Vector4 Specular { get; set; }

        // Proprietati noi pentru atenuare 
        // Valori default pentru nicio atenuare: Constant=1, Linear=0, Quadratic=0
        public float ConstantAttenuation { get; set; } = 1.0f;
        public float LinearAttenuation { get; set; } = 0.0f;
        public float QuadraticAttenuation { get; set; } = 0.0f;

        // Constructor
        public SursaDeLumina(LightName name)
        {
            this.LightName = name;
            this.IsEnabled = false;

            // Valori default
            this.Ambient = new Vector4(0.1f, 0.1f, 0.1f, 1.0f);
            this.Diffuse = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            this.Specular = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            this.Position = new Vector4(0.0f, 50.0f, 0.0f, 1.0f);
        }

        public void Toggle() { IsEnabled = !IsEnabled; }
        public void Enable() { IsEnabled = true; }
        public void Disable() { IsEnabled = false; }

        public void Apply()
        {
            if (IsEnabled)
            {
                GL.Enable((EnableCap)LightName);
                GL.Light(LightName, LightParameter.Ambient, Ambient);
                GL.Light(LightName, LightParameter.Diffuse, Diffuse);
                GL.Light(LightName, LightParameter.Specular, Specular);
                GL.Light(LightName, LightParameter.Position, Position);

                // Aplicare atenuare
                GL.Light(LightName, LightParameter.ConstantAttenuation, ConstantAttenuation);
                GL.Light(LightName, LightParameter.LinearAttenuation, LinearAttenuation);
                GL.Light(LightName, LightParameter.QuadraticAttenuation, QuadraticAttenuation);
            }
            else
            {
                GL.Disable((EnableCap)LightName);
            }
        }

        public void Move(float x, float y, float z)
        {
            Position = new Vector4(Position.X + x, Position.Y + y, Position.Z + z, 1.0f);
        }
    }
}

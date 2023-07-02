using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.umbreon.sapphire
{
    public partial class SpecialMaterial : StandardMaterial3D
    {

        public void asdf()
        {
            var id = this._GetShaderRid();
            var sha = RenderingServer.ShaderGetCode(id);
            sha.Replace("fragment()", "s3d()");
            sha += "void fragment() {\n" +
                "s3d();\n" + 
                "ALPHA = 0.5f;\n" +
                "}\n";
            RenderingServer.ShaderSetCode(id, sha); 
        }


    }
}

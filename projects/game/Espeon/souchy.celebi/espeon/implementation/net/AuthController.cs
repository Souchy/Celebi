using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Espeon.souchy.celebi.espeon.implementation.net
{
    public class AuthController
    {

        public void handleSignin(SigninPacket p)
        {
            // call spark
            // ask it to check that the token is valid
            // return account info if it is
        }

        public void handleSignout()
        {

        }

    }


    public class SigninPacket
    {
        public string token { get; set; }
    }


}

/*This file is part of PassGen
 * Copyright (C) 2009,2010  Daniel Myers dan<at>moird.com
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>
 */
using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections;

namespace PassGen
{
    class password
    {
        #region Fields
        private byte m_groupid;
        private Random Rand;
        private Stack m_groupstackidx = new Stack();
        #endregion //Fields

        #region CharacterDefinitions
        private readonly char[][] m_pwgroups = new char[][]
            {
                "abcdefghijklmnopqrstuvwxyz".ToCharArray(),
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(),
                "0123456789".ToCharArray(),
                "()`~!@$%^*-+=|\\{}[]:;\"'<>,.?/_#".ToCharArray()
            };
        #endregion //CharacterDefinitions


        #region Constructor
        public password()
        {
            byte[] randomBytes = new byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            int seed = (randomBytes[0] & 0x7f) << 24 |
                       randomBytes[1] << 16 |
                       randomBytes[2] << 8 |
                       randomBytes[3];
            Rand = new Random(seed);
        }
        #endregion //Constructor

        #region PublicMethods
        public string genpass(int pwlength, bool pwusespecial)
        {
            if (pwusespecial) m_groupid = 4; // use special characters
            else m_groupid = 3; // Do not use Special Characters
            establishStack(pwlength); //Initialize stack

            StringBuilder passwd = new StringBuilder(pwlength);
            for (int i = 0; i < pwlength; i++)
            {
                int groupidx = (int)m_groupstackidx.Pop();
                int grouplength = m_pwgroups[groupidx].Length;
                char c = m_pwgroups[groupidx][Rand.Next(grouplength)];

                passwd.Append(c);
            }
            return passwd.ToString();
        }
        #endregion //Methods

        #region PrivateMethods
        private void establishStack(int pwlength)
        {
            while(m_groupstackidx.Count != pwlength)
            {
                int tmpgroupidx = Rand.Next(m_groupid);
                if(m_groupstackidx.Count == 0) m_groupstackidx.Push(tmpgroupidx);
                else if (tmpgroupidx != (int)m_groupstackidx.Peek()) m_groupstackidx.Push(tmpgroupidx);
            }
        }

        #endregion //Private Methods
    }
}

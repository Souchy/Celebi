﻿using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.net.sapphire
{
    public interface PacketForfeit
    {
        /// <summary>
        /// Only need it from server to client
        /// </summary>
        public IID playerId { get; set; }
    }
}
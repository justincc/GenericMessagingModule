/*
 * Copyright (c) Contributors, http://opensimulator.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the OpenSimulator Project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE DEVELOPERS ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;
using Mono.Addins;
using Nini.Config;
using OpenSim.Framework;
using OpenSim.Region.Framework.Interfaces;
using OpenSim.Region.Framework.Scenes;

[assembly: Addin("GenericMessagingModule", "0.1")]
[assembly: AddinDependency("OpenSim", "0.5")]

namespace EventRecorder
{
    [Extension(Path = "/OpenSim/RegionModules", NodeName = "RegionModule", Id = "GenericMessagingModule")]
    public class EventRecordingModule : ISharedRegionModule
    {
        private static readonly ILog m_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);                
        
        public string Name { get { return "Generic Messaging Demo Module"; } }        
        
        public Type ReplaceableInterface { get { return null; } }
        
        public void Initialise(IConfigSource source)
        {
            m_log.DebugFormat("[GENERIC MESSAGING]: INITIALIZED MODULE");
        }
        
        public void PostInitialise()
        {
            m_log.DebugFormat("[GENERIC MESSAGING]: POST INITIALIZED MODULE");
        }
        
        public void Close()
        {
            m_log.DebugFormat("[GENERIC MESSAGING]: CLOSED MODULE");
        }
        
        public void AddRegion(Scene scene)
        {
            scene.EventManager.OnNewClient += HandleNewClient;
        }

        private void HandleNewClient(IClientAPI client)
        {
            client.AddGenericPacketHandler("test", HandleGenericMessage);
        }

        private void HandleGenericMessage(object sender, string method, List<string> args)
        {
            IClientAPI client = (IClientAPI)sender;

            m_log.DebugFormat(
                "[GENERIC MESSAGE]: Received message with method {0}, args {1} from {2} in {3}", 
                method, string.Join("|", args.ToArray()), client.Name, client.Scene.Name);
        }
        
        public void RemoveRegion(Scene scene)
        {
            m_log.DebugFormat("[GENERIC MESSAGING]: REGION {0} REMOVED", scene.RegionInfo.RegionName);
        }        
        
        public void RegionLoaded(Scene scene)
        {
            m_log.DebugFormat("[GENERIC MESSAGING]: REGION {0} LOADED", scene.RegionInfo.RegionName);
        }                
    }
}
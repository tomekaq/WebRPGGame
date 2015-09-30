using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;

namespace WebRPGGame.Code
{
    public class FightLogContainer
    {
        public static ConcurrentDictionary<string, ConcurrentQueue<string>> logs = 
            new ConcurrentDictionary<string, ConcurrentQueue<string>>();

        public static void Add(string fightId, string message)
        {
            ConcurrentQueue<string> log;
            if (!logs.TryGetValue(fightId, out log))
            {
                log = new ConcurrentQueue<string>();
                logs.TryAdd(fightId, log);
            }

            log.Enqueue(message);
        }

        public static List<string> Get(string fightId)
        {
            ConcurrentQueue<string> log;
            if (!logs.TryGetValue(fightId, out log))
            {
                return new List<string>();
            }
            var list = new List<string>();
            string msg;
            while (log.TryDequeue(out msg))
            {
                list.Add(msg);
            }
            return list;
        }
    }
}
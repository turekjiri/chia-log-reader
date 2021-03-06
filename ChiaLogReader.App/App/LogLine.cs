using System;
using System.Globalization;

namespace ChiaLogReader.App.App
{
    public class LogLine
    {
        public DateTime Date { get; set; }
        public Who Who { get; set; }
        public Severity Severity { get; set; }
        public MessageType MessageType { get; set; }
        public string Message { get; set; }

        public string OriginalMessage { get; set; }

        public LogLine(string line)
        {
            if (line.Contains("INFO") || line.Contains("WARNING") || line.Contains("ERROR"))
            {
                var splitted = line.Split(" ");

                Date = DateTime.ParseExact(splitted[0], "yyyy-MM-ddTHH:mm:ss.fff", CultureInfo.InvariantCulture);
                Who = (Who)Enum.Parse(typeof(Who), splitted[1]);
                Severity = GetSeverity(line);
                MessageType = GetMessageType(line);
            }

            OriginalMessage = line;

        }

        private MessageType GetMessageType(string txt)
        {
            var moddedTxt = txt.ToLower()
                                .Replace(" ", "_")
                                .Replace(",", "")
                                .Replace("'", "");

            foreach (MessageType type in (MessageType[])Enum.GetValues(typeof(MessageType)))
                if (moddedTxt.Contains(type.ToString()))
                    return type;

            return MessageType.unknown;
        }

        private Severity GetSeverity(string txt)
        {
            foreach (Severity severity in (Severity[])Enum.GetValues(typeof(Severity)))
                if (txt.Contains(severity.ToString()))
                    return severity;

            return Severity.unknown;
        }
    }

    public enum Who
    {
        unknown,
        full_node,
        harvester,
        wallet,
        farmer,
    }

    public enum Severity
    {
        unknown,
        INFO,
        WARNING,
        ERROR
    }

    public enum MessageType
    {
        unknown,
        respond_block_to_peer, // respond_block to peer
        new_peak_from_peer, //new_peak from peer
        new_transaction,
        respond_transaction,
        request_transaction,
        new_signage_point,
        respond_signage_point,
        request_signage_point_or_end_of_sub_slot,
        new_signage_point_or_end_of_sub_slot,
        new_compact_vdf,
        request_compact_vdf,
        respond_compact_vdf,
        new_unfinished_block,
        new_peak,
        request_block,
        respond_block,
        new_signage_point_harvester,
        farming_info,
        eligible,
        peer,
        add_spendbundle,
        not_accepting_inbound_connection,
        finished_signage_point,
        updated_wallet_peak_to_height,
        it_took,
        added_unfinished_block,
        updated_peak_to_height,
        connection_closed,
        dont_have_rc_hash,
        not_added_cc_challenge,
        duplicate_compact_proof,
        consensus_error,
        wasnt_able_to_add_transaction,
        already_compactified_block,
        dont_have_signage_point,
        searching_directories,
        loaded_a_total_of,
        cannot_connect_to_host,
        finished_sub_slot,
        cannot_write_to_closing_transport,
        connected_with_full_node,
        received_orphan_block,
        recent_chain,
        index_from_challenge,
        last_rc_infusion
    }
}

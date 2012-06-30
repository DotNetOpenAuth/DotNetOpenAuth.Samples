using System.IO;

public class TracePageAppender : log4net.Appender.AppenderSkeleton {
	protected override void Append(log4net.Core.LoggingEvent loggingEvent) {
		StringWriter sw = new StringWriter(Logging.LogMessages);
		Layout.Format(sw, loggingEvent);
	}
}

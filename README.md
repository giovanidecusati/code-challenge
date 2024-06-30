# HTTP Parser
To run this console app, build and type the following command:

![assets\results.png](assets\executing.png)

Unit Test Results
![assets\results.png](assets\results.png)

```
.\HttpLogParser.ConsoleApp.exe -f "C:\temp\Mantel\programming-task\programming-task-example-data.log"
```


## Assumptions:
Given the following log sample:

```
177.71.128.21 - - [10/Jul/2018:22:21:28 +0200] "GET /intranet-analytics/ HTTP/1.1" 200 3574 "-" "Mozilla/5.0 (X11; U; Linux x86_64; fr-FR) AppleWebKit/534.7 (KHTML, like Gecko) Epiphany/2.30.6 Safari/534.7"
50.112.00.11 - admin [11/Jul/2018:17:31:56 +0200] "GET /asset.js HTTP/1.1" 200 3574 "-" "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/536.6 (KHTML, like Gecko) Chrome/20.0.1092.0 Safari/536.6"
```

1. Spaces are used to separate some fields;
2. Timestamp is represented by square bracket ([]);
2. Null values are represented by dash (-);
3. String are represented by double-quotes (");
4. There are 3 types of IIS log format:
	1. W3C is the default IIS log format and lets you choose which fields to include.
	2. IIS format is less flexible, as you cannot customize the fields. However, the file format is CSV (;).
	3. NCSA format is another fixed format, and it does not allow customizing event fields. Its simpler than the IIS and W3C formats, containing only basic information like username, time, request type, and HTTP status code.
5. The log sample provided looks using NCSA-ish because the representation of the fields are "fixed". However, it doesn't follow the same column order specified by the NCSA format.

## 1. As a [SYSTEM] I want to be able to receive a log entry as a parameter so that I can parse the log information into model;
1. Receive the log path as parameter;
2. Read all log entries based on the file;
3. Parse the log content using the configuration into model and return;
### Acceptence Criteria:
1. Given a log entry I should be able to parse its structure, so then I can return a model with all log information;

## 2. As a [USER] I want to be able to parse a log file, so that I can report on its contents:
1. Provide a path of log file as parameter;
2. Parse the log file in a model;
3. Aggregate the data to support reporting;

### Acceptence Criteria:
1. Given a log file I should be able to report on its contents, so then I can read "The number of unique IP addresses";
2. Given a log file I should be able to report on its contents, so then I can read "The top 3 most visited URLs";
3. Given a log file I should be able to report on its contents, so then I can read "The top 3 most active IP addresses";

## Notes
1. 1 Hour for planing;
2. 3 Hours for developing and testing;

## References:
1. [CrowdStrike - Cyber security 101 - Observability Logs](https://www.crowdstrike.com/cybersecurity-101/observability/iis-logs/)
1. [Microsoft - NCSA Logging](https://learn.microsoft.com/en-us/windows/win32/http/ncsa-logging)
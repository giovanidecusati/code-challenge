## HTTP Parser

Given the following log sample:

```
177.71.128.21 - - [10/Jul/2018:22:21:28 +0200] "GET /intranet-analytics/ HTTP/1.1" 200 3574 "-" "Mozilla/5.0 (X11; U; Linux x86_64; fr-FR) AppleWebKit/534.7 (KHTML, like Gecko) Epiphany/2.30.6 Safari/534.7"
50.112.00.11 - admin [11/Jul/2018:17:31:56 +0200] "GET /asset.js HTTP/1.1" 200 3574 "-" "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/536.6 (KHTML, like Gecko) Chrome/20.0.1092.0 Safari/536.6"
```

## Assumptions:
1. No spaces are allowend. Spaces are used to separate fields;
2. Timestamp is represented by square bracket ([]);
2. Null values are represented by dash (-);
3. String are represented by double-quotes (");
4. There are 3 types of log format:
	1. W3C is the default IIS log format and lets you choose which fields to include.
	2. IIS format is less flexible, as you can’t customize the fields included. However, the file format is CSV (;).
	3. NCSA format is another fixed format, and it does not allow customizing event fields. It’s simpler than the IIS and W3C formats, containing only basic information like username, time, request type, and HTTP status code.
5. The log sample provided looks using NCSA because the representation of the fields are "fixed". However, it doesn't follow the same column order specified by the NCSA format.

## 1. As a [SYSTEM] I want to be able to read log configuration from settings, so that I can read the log file;
1. Read column configurations from settings;
### Acceptence Criteria:
1. Given a log format I should be able to configure its structure, so then it can be used to read the log;

## 2. As a [SYSTEM] I want to be able to receive a log entry as a parameter so that I can parse the log information into model;
1. Receive the log entry;
2. Receive the log configuration;
3. Parse the log content using the configuration into model and return;
### Acceptence Criteria:
1. Given a log entry I should be able to parse its structure, so then I can retur a model with all log information;

## 3. As a [USER] I want to be able to parse a log file, so that I can report on its contents:
1. Receive a log file as input;
2. Read log configuration from parameter;
3. Read all rows;
4. Parse each row;
5. Aggregate the data;

### Acceptence Criteria:
1. Given a log file I should be able to report on its contents, so then I can read "The number of unique IP addresses";
2. Given a log file I should be able to report on its contents, so then I can read "The top 3 most visited URLs";
3. Given a log file I should be able to report on its contents, so then I can read "The top 3 most active IP addresses";

## Notes
1. 1 Hour for planing;
2. 3 Hours for developing and testing;
3. US1 we could read from appsettings.json or whathereve structure;
4. US3 we could use a console library such as 

## References:
1. [CrowdStrike - Cyber security 101 - Observability Logs](https://www.crowdstrike.com/cybersecurity-101/observability/iis-logs/)
1. [Microsoft - NCSA Logging](https://learn.microsoft.com/en-us/windows/win32/http/ncsa-logging)
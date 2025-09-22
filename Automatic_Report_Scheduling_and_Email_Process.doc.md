# Automatic Process for Scheduling Reports and Sending Them Through Email

---


![Untitled Diagram-Page-1](https://github.com/user-attachments/assets/e8c1dc61-7dba-438d-af43-c9ae1ff27d02)



## Step 1: Trigger Stored Procedure Execution (AWS Lambda Scheduler)

**1. Use AWS EventBridge (CloudWatch Events) to schedule a Lambda function.**
- AWS EventBridge (formerly CloudWatch Events) is a service that allows you to trigger actions on a schedule or in response to events.
- You can create a rule in EventBridge using a cron or rate expression (for example, every day at 8 AM).
- This rule can trigger an AWS Lambda function automatically—no manual intervention required.
- The Lambda function can then execute any code you want, such as running a stored procedure, generating a report, or starting another workflow.
- This setup is serverless, highly reliable, and easy to maintain for scheduled automation tasks.

**2. This Lambda function triggers the execution of your stored procedure (SP) in the database (e.g., SQL Server, RDS).**
- The scheduled AWS Lambda function connects to your database (such as SQL Server or Amazon RDS).
- It executes the desired stored procedure (SP) by sending a command/query to the database.
- The Lambda function can use environment variables to securely store database connection details (host, user, password).
- After triggering the stored procedure, the Lambda function can log the execution status or job ID for tracking.
- This allows the report generation or data processing task to start automatically, as part of your automated workflow.

**3. The Lambda acts as a “fire and forget” initiator—it only starts the job and exits.**
- The Lambda function acts as a “fire and forget” initiator.
- It sends a command to start the stored procedure or job in the database.
- The Lambda does not wait for the stored procedure to finish or process the results.
- Once the job is triggered, the Lambda function exits immediately.
- This approach allows for efficient, asynchronous processing without holding up Lambda resources.

---

## Step 2: Monitor Stored Procedure Completion

**1. Maintain trigger information in a dedicated SQL table for auditing and tracking.**
- The table should capture:
  - The date and time the stored procedure was triggered (`TriggerDate`)
  - The name of the stored procedure (`StoredProcedureName`)
  - The execution status (`ExecutionStatus`, e.g., Started, Completed, Failed)
  - A unique identifier for each trigger event (`TriggerId`, as a GUID)
- This helps track, monitor, and debug the automated process.

**Example Table:**

| TriggerId                                | TriggerDate     | StoredProcedureName    | ExecutionStatus | File location                                               | Remarks                              |
|------------------------------------------|-----------------|-----------------------|----------------|-------------------------------------------------------------|--------------------------------------|
| 6D2F2D9E-1E4B-4A67-8BFE-1E9C7E1A1C1A     | 9/22/2025 8:00  | usp_GenerateDailyReport | Started        | s3://report-exports/daily/2025-09-22/report_2025-09-22.csv  | Triggered by Lambda Scheduler        |
| 7C1E6B7B-1C1C-4E9F-92F8-2A1B9F3E2B2D     | 9/22/2025 8:01  | usp_GenerateDailyReport | Completed      | s3://report-exports/daily/2025-09-22/report_2025-09-22.xlsx | Report generated successfully        |
| 8A3F5C8D-2D3D-4F7E-91C2-3B4F2D6E3C3F     | 9/21/2025 8:00  | usp_GenerateWeeklySummary | Started     | s3://report-exports/daily/2025-09-22/report_2025-09-22.pdf  | Triggered by Lambda Scheduler        |
| 9B4D6E9F-3E5E-4A8F-93D4-4C6F2E7F4D4E     | 9/21/2025 8:02  | usp_GenerateWeeklySummary | Failed      | s3://report-exports/daily/2025-09-22/report_2025-09-22.pdf  | Timeout error during execution       |

**2. A second Lambda function, scheduled (e.g., every minute), checks a status flag or result table to determine if the SP has finished.**

**3. Once the SP completes, the Lambda fetches the results from a temporary/results table.**
- Once the stored procedure (SP) completes its execution, the results are available in a temporary or results table in the database.
- The Lambda function connects to the database and queries the results table to retrieve the output data.
- This approach ensures that the Lambda only processes data after the SP has finished and results are ready.
- The retrieved results can then be further processed, exported (e.g., to CSV), or uploaded to another storage location like Amazon S3.
- The Lambda exports the result to a CSV file and uploads it to an AWS S3 bucket.

---

## Step 3: Generate PDF or Excel from CSV

**1. Reads the CSV from S3.**
- The Lambda function reads the CSV file stored in the specified S3 bucket location.
- It uses AWS SDK (such as Boto3 for Python or AWS SDK for .NET) to connect to Amazon S3.
- The Lambda function downloads or streams the CSV file content from S3.
- The CSV data is then parsed and processed as needed (e.g., for further transformation, validation, or loading into another system).
- This approach enables automated, serverless processing of report files generated by the stored procedure.

**2. Converts it to Excel and/or PDF format (this can be handled more robustly with a C# program running on AWS Lambda [using .NET], AWS Fargate, or EC2 if Lambda size is a limitation).**
- After reading the CSV file from S3, the data is converted to Excel (XLSX) and/or PDF format.
- This conversion can be performed using a C# program, which can run on AWS Lambda (using .NET), AWS Fargate, or EC2.
- Using AWS Lambda is serverless and cost-effective for small-to-medium files; however, Lambda has memory and execution time limits.
- For larger files or more complex conversion tasks, AWS Fargate (containerized apps) or EC2 (virtual machines) provide more resources and flexibility.
- The converted Excel or PDF file can then be uploaded back to S3 for storage or further processing.

**3. Exports the resulting Excel and PDF files back to S3.**
- Once the CSV data is converted to Excel and/or PDF format, the Lambda function (or C# program) uploads these files back to Amazon S3.
- The files are saved in a designated S3 bucket and folder structure, often using a job GUID or timestamp to ensure uniqueness and easy retrieval.
- Storing the exported Excel and PDF files in S3 allows for secure, scalable, and reliable access by other systems or users.
- S3 storage enables further actions, such as sharing download links, triggering notifications, or initiating additional workflows.

---

## Step 4: Email the Results

A C# program (could be a Lambda function or containerized app) does the following: 

**1. Fetches the Excel and PDF from S3.**
- The Lambda function (or another AWS service) fetches the generated Excel (.xlsx) and PDF files from the specified Amazon S3 bucket location.
- It uses the AWS SDK (e.g., Boto3 for Python, AWS SDK for .NET) to connect to S3 and retrieve the files.
- The files can be downloaded or streamed for further processing, sharing, or distribution.
- Fetching from S3 ensures reliable and scalable access to the exported reports.

**2. Sends an email (using AWS SES or SMTP), attaching the files.**
- After fetching the Excel and PDF files from S3, the Lambda function (or another AWS service) sends an email to intended recipients.
- The email is sent using AWS Simple Email Service (SES) or an SMTP server.
- The Excel and PDF files are attached to the email.
- AWS SES provides a scalable, reliable, and cost-effective way to send emails programmatically from Lambda or other AWS compute services.
- SMTP can be used for integration with traditional mail servers if SES is not suitable.
- This step ensures that the generated reports are automatically delivered to stakeholders or users.

---

## Step 5: Reporting Types 

**1. Dynamically Generated Reports (via Stored Procedure Output)**
- These reports are created on-the-fly based on the output of a stored procedure.
- The column aliases and data structure are defined dynamically by the stored procedure at runtime.
- This approach provides flexibility, allowing the report format and fields to change depending on parameters or business logic.
- Example: Exporting data to CSV/Excel/PDF based on the direct output of a stored procedure.

**2. SSRS Default Template Reports (Fixed/Unique Templates)**
- These reports are generated using predefined SQL Server Reporting Services (SSRS) templates.
- Each template is unique, with a fixed layout, formatting, and data fields.
- The templates are typically designed in advance for specific business needs (e.g., invoices, summary reports, dashboards).
- Example: Generating a PDF report using an SSRS template for a monthly financial summary.

---

## Step 6: Configurations

- All report emails and stored procedure details are centrally configured in either a SQL table or Lambda configuration.
- SQL connection strings are stored as part of the configuration, enabling Lambda or other services to connect to the appropriate database.
- Time frames or scheduling information (e.g., daily, weekly, at specific times) are included in the configuration to automate report generation.
- Recipient email addresses for each report are maintained in the configuration, allowing easy updates and management.
- Stored procedure names are specified, so the correct stored procedure is executed for each report.
- Lambda function names or identifiers may be included for dynamic invocation and modularity.
- S3 output paths are configured to control where generated reports are stored and retrieved.
- Centralized configuration enables dynamic scheduling, execution, and delivery of reports without code changes.
- This setup allows easy scaling, maintenance, and auditing of all report-related operations.

---

## Step 7: Validate the Results

- Perform post-send validation: 
  - Confirm files exist in S3.
  - Log email delivery status (SES provides feedback).
  - Optionally, email or log a summary report of the process.

---

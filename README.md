# GIC Assessment Full Stack

## Project Overview

This project consists of a frontend and backend, deployed to AWS:

**Frontend**: Hosted on AWS S3 as a static website.

**Backend**: Hosted on AWS EC2.

Database: MS SQL Server (hosted externally, not on AWS).

All secrets and sensitive data are stored securely in **GitHub Secrets**. CI/CD pipelines are configured to **automatically deploy** both frontend and backend to AWS.

## Local Setup

### Backend

## Install .NET 8.x SDK from dotnet.microsoft.com.

## Clone the repository:

```bash
git clone https://github.com/kapilarathnayaka/gicassesmentfullstack.git
cd gicassesmentfullstack/backend
```

## Set up the database connection string:

Add CONNECTION_STRING="your-connection-string" to a .env file.

OR add it to ~/.bashrc:

```bash
export CONNECTION_STRING="your-connection-string"
source ~/.bashrc
```

## Run the backend:

```bash
dotnet restore
dotnet run
```

## Run with Docker:

```bash
docker build -t gic-backend .
docker run -d -p 8080:80 --name gic-container -e CONNECTION_STRING="$CONNECTION_STRING" gic-backend
```

Access backend locally: http://localhost:8080/swagger/index.html

### Frontend

## Install Node.js from nodejs.org.

## Navigate to frontend directory:
```bash
cd gicassesmentfullstack/frontend
```

## Install dependencies:
```bash
npm install
```
## Run the frontend:
```bash
npm start
```

### Deployment to AWS

## CI/CD Pipeline

- GitHub Actions deploys frontend to AWS S3 and backend to AWS EC2 automatically.

- Database connection string is securely stored in GitHub Secrets:

     - CONNECTION_STRING: MS SQL database connection.

     - AWS_ACCESS_KEY_ID & AWS_SECRET_ACCESS_KEY: AWS authentication.

- Access the Application

   **Frontend**: <a href="http://gictests3.s3-website-us-east-1.amazonaws.com" target="_blank" style="text-decoration: none; color: blue;">Demo Frontend</a>

   **Backend API**: <a style="text-decoration: none; color: blue;" href="http://54.82.91.72:8080/swagger/index.html" target="_blank">Swagger API</a>

### Future Enhancements
- Kubernetes for auto-scaling EC2 instances.

- AWS ALB (Application Load Balancer) with ECS for load balancing.

- AWS Route 53 for custom domain management.

- Enhanced security: Firewall rules and security policies for request filtering.

***~EOF~***


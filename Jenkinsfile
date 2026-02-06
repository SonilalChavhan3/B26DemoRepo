		pipeline {
    agent any
		environment {
        DOTNET_ROOT = "C:\\Program Files\\dotnet"
        SOLUTION_NAME = "B26DemoRepo.sln"
        PROJECT_PATH = "WebApplication\\WebApplication.csproj"
        NEXUS_URL = "http://localhost:8081/repository/Batch25/"
        PS_SCRIPT_PATH = "C:\\Tools\\commonbuild\\NugetPackagePublish.ps1"
        Project_Name = "WebApplication"
        
    }
stages {
	stage('Checkout') {
            steps {
                echo "[${new Date().format('HH:mm:ss')}] Cleaning workspace..."
              
                checkout scm
            }
        }
	 stage('Restore Packages') {
            steps {
                echo "Restoring NuGet packages..."
                bat "dotnet restore ${env.SOLUTION_NAME}"
            }
        }
        stage('Build') {
            steps {
                   echo "⚙️ Building .NET project..."
                bat "dotnet build ${env.PROJECT_PATH} -c Release --no-restore"
            }
        }

	  stage('SonarQube Analysis') {
			            steps {
			                script {
			                    // Assign tool inside script block
			                    def scannerHome = tool 'SonarScanner for MSBuild'
			
			                    // Use withSonarQubeEnv inside script block
			                    withSonarQubeEnv('MySonarQube') {
			                        bat "\"${scannerHome}\\SonarScanner.MSBuild.exe\" begin /k:\"B26Demo\""
			                        bat "dotnet build"
			                        bat "\"${scannerHome}\\SonarScanner.MSBuild.exe\" end"
			                    }
			                }
			            }
			        }

stage('Test') {
            steps {
                echo 'Testing...'
            }
        }

stage('Deploy') {
            steps {
                echo 'Deploying...'
            }
        }
    }
}

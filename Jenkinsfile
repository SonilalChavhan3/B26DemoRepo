		pipeline {
    agent any
		environment {
        DOTNET_ROOT = "C:\\Program Files\\dotnet"
        SOLUTION_NAME = "B26DemoRepo.sln"
        PROJECT_PATH = "WebApplication\\WebApplication.csproj"
        NEXUS_URL = "http://localhost:8081/repository/B26DemoNexus/"
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

stage('SonarQube Analysis and Testing..') {
    steps {
        script {
            def scannerHome = tool 'SonarScanner for MSBuild'

            withSonarQubeEnv('MySonarQube') {
                // Step 1: Sonar Begin
                bat """
                \"${scannerHome}\\SonarScanner.MSBuild.exe\" begin ^
                    /k:\"${env.Project_Name}_${env.BRANCH_NAME}\" ^
                    /n:\"${env.Project_Name} (${env.BRANCH_NAME})\" ^
                    /v:\"${env.BUILD_NUMBER}\" ^
                    /d:sonar.cs.opencover.reportsPaths=\"**/coverage.opencover.xml\" ^
                    /d:sonar.coverage.exclusions=\"**/*Migrations*/**\"
                """

                // Step 2: Build
                bat "dotnet build ${env.SOLUTION_NAME} -c Release"

				echo 'Testting...'
                // Step 3: Test with Coverage
               bat """
    dotnet test ${env.SOLUTION_NAME} ^
        --settings \"${WORKSPACE}\\coverlet.runsettings\" ^
        --logger \"trx;LogFileName=TestResults.trx\" ^
        /p:CollectCoverage=true ^
        /p:CoverletOutput=\"${WORKSPACE}\\TestResults\\coverage.opencover.xml\" ^
        /p:CoverletOutputFormat=opencover
    """

                // Step 4: Sonar End
                bat "\"${scannerHome}\\SonarScanner.MSBuild.exe\" end"
            }
        }
    }
}
	 stage('Create and Push NuGet Package') {
            steps {
                script {
                    powershell """
                        powershell.exe -NonInteractive -ExecutionPolicy Bypass `
                        -File \"${env.PS_SCRIPT_PATH}\" `
                        -ProjectName \"${env.Project_Name}\" `
						-ProjectPath \"${env.PROJECT_PATH}\" `
                        -BranchName \"${env.BRANCH_NAME}\" `
                        -BuildNumber \"${env.BUILD_NUMBER}\" `
                        -NexusUrl \"${env.NEXUS_URL}\" 
                    """
                    
                }
            }
        }

stage('Deploy') {
            steps {
                echo 'Deploying...'
            }
        }
    }
}

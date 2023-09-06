# Define the image name
$imageName = "test-postgres-hangman-image"

# Define the container name
$containerName = "test-postgres-hangman-container"

# Build the Docker image
docker build -t $imageName -f .\TestData\Dockerfile .

# Check if a container with the same name already exists
$containerExists = docker ps -a --format '{{.Names}}' | Where-Object { $_ -eq $containerName }

# If a container with the same name exists, remove it
if ($containerExists) {
    docker rm -f $containerName
}

# Run a container from the image
docker run -p 5432:5432 --name $containerName $imageName
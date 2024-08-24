#!/bin/bash

# Proje dizinine gidin
cd "$(dirname "$0")"

# Docker Compose ile servisleri başlatın
echo "Starting Docker Compose services..."
docker-compose up --build -d

# Servislerin başlatılmasını bekleyin
echo "Waiting for services to start..."
sleep 10 # Servislerin başlaması için bekleme süresi

# Veritabanı seeding (Başlatma verileri) işlemi (İsteğe bağlı)
# MongoDB veya Redis'te başlangıç verileri oluşturulabilir.
# echo "Seeding initial data to MongoDB..."
# docker exec -i mongodb mongo <path_to_initial_data_script>.js

# Başarılı başlatma mesajı
echo "Project initialization completed. Access your app at http://localhost:5000"

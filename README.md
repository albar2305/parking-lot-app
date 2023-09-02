# Sistem Parkir Console App

Ini adalah aplikasi konsol sederhana untuk mengelola sistem parkir. Aplikasi ini dibangun dengan .NET 5 dan memungkinkan Anda membuat lot parkir, memarkir kendaraan, mengosongkan slot, dan melakukan berbagai operasi lainnya.

## Petunjuk Penggunaan

Untuk menjalankan aplikasi, Anda perlu menjalankannya dalam lingkungan .NET. Berikut adalah langkah-langkah dasar untuk menggunakan aplikasi ini:

1. Buka terminal atau command prompt.

2. Beralih ke direktori proyek Anda.

3. Jalankan aplikasi dengan perintah:
   dotnet run
4. Anda dapat memasukkan perintah-perintah berikut untuk mengelola sistem parkir:

- `create_parking_lot <jumlah_slot>`: Membuat lot parkir dengan jumlah slot tertentu.

- `park <nomor_plat> <warna> <jenis_kendaraan>`: Memarkir kendaraan dalam lot parkir.

- `leave <nomor_slot>`: Mengosongkan slot parkir.

- `status`: Menampilkan status lot parkir.

- `type_of_vehicles <jenis_kendaraan>`: Menampilkan jumlah kendaraan berdasarkan jenis kendaraan.

- `registration_numbers_for_vehicles_with_ood_plate`: Menampilkan nomor plat dengan digit terakhir yang ganjil.

- `registration_numbers_for_vehicles_with_event_plate`: Menampilkan nomor plat dengan digit terakhir yang genap.

- `registration_numbers_for_vehicles_with_colour <warna>`: Menampilkan nomor plat kendaraan berdasarkan warna.

- `slot_numbers_for_vehicles_with_colour <warna>`: Menampilkan nomor slot kendaraan berdasarkan warna.

- `slot_number_for_registration_number <nomor_plat>`: Menampilkan nomor slot berdasarkan nomor plat kendaraan.

- `exit`: Keluar dari aplikasi.

## Struktur Proyek

- `Program.cs`: Program utama yang menjalankan aplikasi konsol.

- `Model`: Direktori yang berisi definisi model kendaraan.

- `Enums`: Direktori yang berisi definisi enum untuk jenis kendaraan.

- `Repository`: Direktori yang berisi definisi repositori lot parkir.

- `Service`: Direktori yang berisi logika layanan parkir.

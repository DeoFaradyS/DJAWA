
# Panduan

### **Clone Repository**

Untuk memulai, clone repository dengan perintah ini:

```bash
https://github.com/DeoFaradyS/DJAWA.git
```

### **Buat Branch**

Setelah meng-clone repository, setiap anggota perlu membuat branch pribadi mereka untuk mengerjakan fitur.

1. **Periksa branch yang ada**:
    ```bash
    git branch
    ```

2. **Pindah ke branch main (jika belum berada di sana)**:
    ```bash
    git checkout main
    ```

3. **Update dengan perubahan terbaru**:
    ```bash
    git pull origin main
    ```

4. **Buat branch baru untuk fitur yang dikerjakan**:
    ```bash
    git checkout -b [nama]-[fitur]
    ```
    Contoh: `deo-create-product`.

5. **Push branch ke GitHub**:
    ```bash
    git push -u origin nama-branch-anggota
    ```

### **Merge ke Main**

Setelah fitur selesai, gabungkan (merge) branch kamu ke `main`:

1. Pindah ke branch main:

```bash
git checkout main
```

2. Update `main` dengan perubahan terbaru:

```bash
git pull origin main
```

3. Merge branch pribadi ke main:

```bash
git merge [nama-branch]
```

4. Jika ada konflik, selesaikan konflik, lalu **add**, **commit**, dan **push** perubahan ke GitHub:

```bash
git add .
git commit -m "Perbaiki konflik"
git push origin main
```

### Pull Request (PR)

Jika menggunakan Pull Request:

1. Buka repo di GitHub.
2. Pilih branch yang ingin di-merge ke main.
3. Klik "New Pull Request", pilih base `main` dan compare dengan branch pribadi.
4. Klik **Create Pull Request**, kemudian **Merge** setelah review.

---

### Jenis-Jenis Commit

### 1. **Tambah Fitur**
```
📦 Add: Tambah fitur create untuk manajemen gudang
```

### 2. **Perbaikan Bug**
```
🐛 Fix: Perbaiki bug pada fitur hapus data
```

### 3. **Refaktor Kode**
```
🔧 Refactor: Refaktor metode login agar lebih mudah dibaca
```

### 4. **Pembaruan** 
```
✏️ Update: Perbarui file README dengan instruksi setup terbaru
```

### 5. **Dokumentasi**
```
📚 Doc: Dokumentasikan endpoint API untuk manajemen pengguna
```

### 6. **Penghapusan Kode** 
```
🔥 Delete: Hapus fungsi yang sudah tidak digunakan lagi
```

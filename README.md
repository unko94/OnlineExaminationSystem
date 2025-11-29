# Online Exam Platform

An ASP.NET Core MVC web application designed to manage online exams with secure authentication, real-time timers, automated grading, and an easy-to-use interface for both students and teachers.

---

##  Features

###  Student Features
- Take exams with **real-time countdown timer**  
- Auto-submit exam when time expires  
- View score and pass/fail result immediately  
- Simple and clean UI  
- Questions loaded dynamically  

###  Teacher/Admin Features
- Create, update, and delete exams  
- Add multiple choice questions with scoring  
- Manage students and exam attempts  
- Real-time notifications when a student submits an exam  
- Dashboard for tracking exam performance  

###  Email System
- Contact Us email using Gmail SMTP and `IEmailSender`  
- Reply-To set to user email so teachers can respond directly  

###  Authentication & Security
- Login, registration, and role management  
- Secure exam access  
- Prevents double submissions  
- Timer protected using **localStorage** to stop cheating  

###  UI & Design
- Bootstrap 
- Responsive design  
- Custom system icon  

---

##  Technologies Used

- **ASP.NET Core MVC 7**
- **Entity Framework Core**
- **SQL Server**
- **Identity Roles & Authentication**
- **SignalR** (real-time teacher notifications)
- **jQuery & AJAX**
- **Bootstrap / Bootswatch**
- **Gmail SMTP (Email Sender)**

---

##  Screenshots

*(Add screenshots here after uploading images)*  
Example:

```
/Screenshots/LoginPage.png  
/Screenshots/StudentExam.png  
/Screenshots/AdminDashboard.png
```

---

##  How to Run the Project

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/OnlineExamPlatform.git
   ```

2. Update these files:
   - `appsettings.json` → Add DB connection + email settings  
   - Ensure SMTP App Password is configured

3. Run the migrations:
   ```bash
   update-database
   ```

4. Run the project:
   ```bash
   dotnet run
   ```

---

##  Contact

For questions, suggestions, or collaborations, feel free to reach out!

---

##  License

This project is open-source and available under the MIT License.

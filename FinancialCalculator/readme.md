
<div dir="rtl"> 
# 📈 Savings & Interest Calculator (Cross-Platform)

מחשבון פיננסי היברידי לחישוב חסכונות לטווח ארוך וריבית דריבית. הפרויקט משלב לוגיקת UI אחת ב-**React** עם מעטפת Native ייעודית ל-Windows ול-Android.



## 🏗 מבנה הפרויקט (Architecture)
המאגר מחולק לתיקיות לפי פלטפורמה, כאשר ה-Frontend משותף לכולן:

* **`/frontend`**: קוד המקור של ה-React (ממשק המשתמש והלוגיקה המתמטית).
* **`/windows`**: פרויקט .NET Framework המשתמש ב-WebView2 להרצת האפליקציה כדסקטופ.
* **`/android`**: פרויקט Android Studio (Kotlin) המשתמש ב-System WebView להרצה בנייד.

## 🛠 טכנולוגיות (Tech Stack)
* **Web:** React.js
* **Desktop:** C# / .NET Framework
* **Mobile:** Kotlin / Android SDK

## 🚀 הרצה ופיתוח
1.  **Frontend:** יש להיכנס לתיקיית `frontend`, להריץ `npm install` ולאחר מכן `npm run build`.
2.  **Windows:** פתחו את קובץ ה-SLN ב-Visual Studio והריצו את הפרויקט.
3.  **Android:** פתחו את תיקיית `android` ב-Android Studio ובצעו Build ל-APK.

## 📄 רישוי (License)
פרויקט זה מופץ תחת רישיון חופשי (**MIT License**). ניתן להשתמש, לשנות ולהפיץ את הקוד ללא הגבלה.

---
**נבנה ככלי עזר לניהול פיננסי אישי.**



</div>

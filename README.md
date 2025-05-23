# Klicko

[![Made With](https://img.shields.io/badge/Made%20with-React%20%2B%20.NET-blue)](https://github.com/Gianlu201/Klicko)
[![Tailwind CSS](https://img.shields.io/badge/Styled%20with-Tailwind%20CSS-38B2AC)](https://tailwindcss.com/)
[![Stripe](https://img.shields.io/badge/Payments-Stripe-blueviolet)](https://stripe.com/)
[![SendGrid](https://img.shields.io/badge/Emails-SendGrid-00b2ff)](https://sendgrid.com/)
[![Demo](https://img.shields.io/badge/Demo-Click_Me-ff0000)](https://klicko.vercel.app/)

---

**Klicko** è una piattaforma e-commerce full stack progettata per offrire agli utenti la possibilità di esplorare e prenotare esperienze e avventure uniche. L'applicazione è sviluppata con un'architettura moderna che combina un frontend dinamico con un backend robusto, garantendo un'esperienza utente fluida e sicura.

---

## 🧭 Indice

- [Demo](#-demo)
- [Panoramica del progetto](#-panoramica-del-progetto)
- [Tecnologie utilizzate](#%EF%B8%8F-tecnologie-utilizzate)
- [Struttura del progetto](#-struttura-del-progetto)
- [Funzioni principali](#%EF%B8%8F-funzioni-principali)
  - [Visitatore](#visitatore)
  - [Utente](#utente)
  - [Venditore](#venditore)
  - [Amministratore](#amministratore)
- [Avvio del progetto in locale](#-avvio-del-progetto-in-locale)
  - [Prerequisiti](#-prerequisiti)
  - [Clonazione del Repository](#-clonazione-del-repository)
  - [Avvio Frontend](#%EF%B8%8F-avvio-frontend)
  - [Avvio Backend](#%EF%B8%8F-avvio-backend)
- [Note](#-note)
- [Anteprima](#-anteprima)
- [Contatti](#-contatti)

---

## 👀 DEMO

È disponibile una demo testabile al seguente [link!](https://klicko.vercel.app/)

Il progetto è stato pubblicato su Vercel (front-end) e su Azure (back-end e database)

Account di prova:

- User User ➡️ user@example.com / useruser (account utente)
- Seller User ➡️ seller@example.com / sellerseller (account venditore)
- Admin User ➡️ **@example.com / ** (account amministratore)

---

## 🌐 Panoramica del Progetto

- **Frontend**: Realizzato con **React.js** e **Tailwind CSS**, offre un'interfaccia utente reattiva e moderna.
- **Backend**: Costruito con **ASP.NET Core** e **SQL Server**, gestisce la logica di business e l'accesso ai dati.
- **Pagamenti**: Integrazione con **Stripe** per transazioni sicure e affidabili.
- **Email**: Utilizzo di **SendGrid** per l'invio di email transazionali e notifiche.

---

## 🛠️ Tecnologie Utilizzate

### Frontend

- [React.js](https://reactjs.org/)
- [Tailwind CSS](https://tailwindcss.com/)
- [React Router](https://reactrouter.com/)

### Backend

- [ASP.NET Core](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Stripe API](https://stripe.com/docs/api)
- [SendGrid API](https://docs.sendgrid.com/)

---

## 📂 Struttura del Progetto

```
Klicko/
├── backend/
│   └── Klicko_be/
│       ├── Controllers/
│       ├── Models/
│       ├── Services/
│       ├── DTOs/
│       └── ...
└── frontend/
    └── Klicko_fe/
        ├── public/
        ├── src/
        │   ├── components/
        │   ├── pages/
        │   ├── App.jsx
        │   └── ...
        └── ...
```

---

## ⚙️ Funzioni Principali

- #### Visitatore

  - **Esplorazione esperienze**: Navigazione tra diverse esperienze e avventure disponibili.
  - **Aggiunta esperienze al carrello**: Possibilità di salvare le esperienze desiderate nel carrello (mediante LocalStorage).
  - **Gestione utenti**: Registrazione, login e gestione del profilo utente.
  - **Registrazione NewsLetter**: Possibilità di iscriversi alla newsletter.

- #### Utente

  - **Acquisti**: Possibilità di acquistare Voucher per riscattare esperienze da vivere.
  - **Prenotazioni**: Possibilità di prenotare esperienze direttamente dalla piattaforma.
  - **Programma fedeltà**: Area riservata che presenta un riepilogo dei bonus volti alla fidelizzazione del cliente.
  - **Area Coupon**: Area riservata che raccoglie i Coupon personali.
  - **Pagamenti sicuri**: Integrazione con Stripe per effettuare pagamenti in modo sicuro.
  - **Notifiche email**: Invio di conferme e notifiche tramite SendGrid.
  - **Dashboard personale**: Area riservata dove poter consultare i propri ordini effettuati.

- #### Venditore

  - **Gestione esperienze**: Possibilità di creare, modificare ed eliminare le esperienze proposte agli utenti.
  - **Protezione esperienze**: Ogni venditore può modificare solo le proprie esperienze.

- #### Amministratore
  - **Pannello admin**: Gestione di tutte le esperienze, visualizzazione degli ordini e controllo degli utenti.
  - **Resoconto**: Visione generale delle statistiche globale come totale delle vendite, acquirenti attivi ed esperienze nascoste.

---

## 🚀 Avvio del Progetto in Locale

### ✅ Prerequisiti

- Node.js (v14.x o superiore)
- .NET SDK (v5.0 o superiore)
- SQL Server
- Account Stripe con chiavi API
- Account SendGrid con chiavi API

### 🔄 Clonazione del Repository

```bash
git clone https://github.com/Gianlu201/Klicko.git
```

---

### ▶️ Avvio Frontend

```bash
cd Klicko/frontend/Klicko_fe
npm install
```

1. Configura le variabili d'ambiente necessarie:

   - Endpoint per le chiamate al server
   - Chiavi API Stripe

2. Avvia l'applicazione:

```bash
npm run dev
```

---

### ▶️ Avvio Backend

```bash
cd Klicko/backend/Klicko_be
```

1. Configura il file `appsettings.json` con:

   - Connection string al database SQL Server

2. Imposta le variabili d'ambiente necessarie:

   - Chiavi API Stripe
   - Chiavi API SendGrid

3. Configura il database dalla Console di gestione pacchetti:

```bash
Update-Database
```

4. Avvia l'applicazione

---

## 💯 Note

- Indicizzazione dell'e-commerce
- SEO sviluppata per una ricerca ottimale sul motore di ricerca Google
- Carrello implementato con l'utilizzo del LocalStorage per utenti non loggati

---

## 📸 Anteprima

_Pagina Home_
![Screen HomePage](screen/screenHomePage.jpg)

_Pagina Esperienze_
![Screen ExperiencesPage](screen/screenExperiencesPage.jpg)

_Pagina Dettagli Esperienza_
![Screen ExperienceDetailPage](screen/screenExperienceDetail.jpg)

_Pagina Carrello_
![Screen CartPage](screen/screenCartPage.jpg)

_Pagina Coupons_
![Screen CouponsPage](screen/screenCouponPage.jpg)

_Pagina CheckOut_
![Screen CheckOutPage](screen/screenCheckOutPage.jpg)

_Pagina Conferma Ordine_
![Screen OrderConfirmationPage](screen/screenOrderConfirmation.jpg)

_Pagina Programma Fedeltà_
![Screen LoyaltyPage](screen/screenLoyaltyPage.jpg)

_Pagina Dashboard Ordini_
![Screen DashboardOrdersPage](screen/screenDashboardOrders.jpg)

_Pagina Vouchers Riscossi_
![Screen RedeemVoucherPage](screen/screenRedeemedVouchers.jpg)

_Pagina Ricerca Voucher_
![Screen RedeemVoucherPage](screen/screenRedeemVoucher.jpg)

_Pagina Dashboard Venditore Esperienze_
![Screen DashboardExperiencesPage](screen/screenDashboardExperiences.jpg)

_Pagina Dashboard Venditore Form Esperienza_
![Screen DashboardExperienceFormPage](screen/screenDashboardExperienceForm.jpg)

_Pagina Dashboard Admin Ordini Ricevuti_
![Screen DashboardAdminPage](screen/screenDashboardAdmin.jpg)

_Pagina Dashboard Admin Gestione Utenti_
![Screen DashboardUsersPage](screen/screenDashboardUsers.jpg)

---

## 📬 Contatti

Per ulteriori informazioni o domande, contattami [@Gianlu201](https://github.com/Gianlu201)

---

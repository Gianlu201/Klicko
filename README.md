# Klicko

[![Made With](https://img.shields.io/badge/Made%20with-React%20%2B%20.NET-blue)](https://github.com/Gianlu201/Klicko)
[![Tailwind CSS](https://img.shields.io/badge/Styled%20with-Tailwind%20CSS-38B2AC)](https://tailwindcss.com/)
[![Stripe](https://img.shields.io/badge/Payments-Stripe-blueviolet)](https://stripe.com/)
[![SendGrid](https://img.shields.io/badge/Emails-SendGrid-00b2ff)](https://sendgrid.com/)
[![Demo](https://img.shields.io/badge/Demo-Click_Me-ff0000)](https://klicko.vercel.app/)

---

**Klicko** è una piattaforma e-commerce full stack progettata per offrire agli utenti la possibilità di esplorare e prenotare esperienze e avventure uniche. L'applicazione è sviluppata con un'architettura moderna che combina un frontend dinamico con un backend robusto, garantendo un'esperienza utente fluida e sicura.

---

## 👀 DEMO

È disponibile una demo testabile al seguente [link!](https://klicko.vercel.app/)

Il progetto è stato pubblicato su Vercel (front-end) e su Azure (back-end e database)

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

## ⚙️ Funzionalità Principali

- **Esplorazione esperienze**: Gli utenti possono navigare tra diverse esperienze e avventure disponibili.
- **Acquisti**: Possibilità di acquistare Voucher per riscattare esperienze da vivere.
- **Prenotazioni**: Possibilità di prenotare esperienze direttamente dalla piattaforma.
- **Programma fedeltà**: Area riservata che presenta un riepilogo dei bonus volti alla fidelizzazione del cliente.
- **Gestione utenti**: Registrazione, login e gestione del profilo utente.
- **Pagamenti sicuri**: Integrazione con Stripe per effettuare pagamenti in modo sicuro.
- **Notifiche email**: Invio di conferme e notifiche tramite SendGrid.
- **Dashboard personale**: Area riservata dove poter consultare i propri ordini effettuati.
- **Pannello admin**: Gestione delle esperienze, visualizzazione delle prenotazioni e controllo degli utenti.

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
npm run preview
oppure
npm run dev
```

---

### ▶️ Avvio Backend

```bash
cd Klicko/backend/Klicko_be
```

1. Configura il file `appsettings.json` con:

   - Connection string al database SQL Server
   - Chiavi API Stripe
   - Chiavi API SendGrid

2. Imposta le variabili d'ambiente necessarie:

   - Chiavi API Stripe

3. Configura il database dalla Console di gestione pacchetti:

```bash
Update-Database
```

4. Avvia l'applicazione

---

## 📬 Contatti

Per ulteriori informazioni o domande, contattami [@Gianlu201](https://github.com/Gianlu201)

---

# Klicko

[![Made With](https://img.shields.io/badge/Made%20with-React%20%2B%20.NET-blue)](https://github.com/Gianlu201/Klicko)
[![Tailwind CSS](https://img.shields.io/badge/Styled%20with-Tailwind%20CSS-38B2AC)](https://tailwindcss.com/)
[![Stripe](https://img.shields.io/badge/Payments-Stripe-blueviolet)](https://stripe.com/)
[![SendGrid](https://img.shields.io/badge/Emails-SendGrid-00b2ff)](https://sendgrid.com/)

---

**Klicko** è una piattaforma e-commerce full stack progettata per offrire agli utenti la possibilità di esplorare e prenotare esperienze e avventure uniche. L'applicazione è sviluppata con un'architettura moderna che combina un frontend dinamico con un backend robusto, garantendo un'esperienza utente fluida e sicura.

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
│       └── ...
└── frontend/
    └── Klicko_fe/
        ├── public/
        ├── src/
        │   ├── components/
        │   ├── pages/
        │   ├── App.js
        │   └── ...
        └── ...
```

---

## ⚙️ Funzionalità Principali

- **Esplorazione esperienze**: Gli utenti possono navigare tra diverse esperienze e avventure disponibili.
- **Prenotazioni**: Possibilità di prenotare esperienze direttamente dalla piattaforma.
- **Gestione utenti**: Registrazione, login e gestione del profilo utente.
- **Pagamenti sicuri**: Integrazione con Stripe per effettuare pagamenti in modo sicuro.
- **Notifiche email**: Invio di conferme e notifiche tramite SendGrid.
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
npm start
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

2. Avvia l'applicazione:

```bash
dotnet run
```

---

## 📬 Contatti

Per ulteriori informazioni o domande, contatta [@Gianlu201](https://github.com/Gianlu201)

---

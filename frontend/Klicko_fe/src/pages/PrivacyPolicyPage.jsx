import React from 'react';

const PrivacyPolicyPage = () => {
  return (
    <div className='max-w-5xl min-h-screen px-6 mx-auto mt-6 mb-8 xl:px-0'>
      <h1 className='mb-3 text-3xl font-bold'>Privacy Policy</h1>

      <div className='px-6 py-5 bg-white border rounded-lg shadow border-gray-400/30'>
        <div className='mb-4'>
          <h3 className='mb-1 text-lg font-semibold'>
            Informativa sulla Privacy
          </h3>
          <p className='text-gray-600 ms-3'>
            La tua privacy è importante per noi. È politica di Avventura Mondo
            rispettare la tua privacy in relazione a qualsiasi informazione che
            possiamo raccogliere durante l'utilizzo del nostro sito web.
          </p>
        </div>

        <div className='mb-4'>
          <h3 className='mb-1 text-lg font-semibold'>
            Informazioni che raccogliamo
          </h3>
          <p className='text-gray-600 ms-3'>
            Raccogliamo informazioni quando ti registri sul nostro sito,
            effettui un acquisto, ti iscrivi alla newsletter o compili un
            modulo. Le informazioni raccolte includono il tuo nome, indirizzo
            email, indirizzo postale, numero di telefono e dati della carta di
            credito.
          </p>
        </div>

        <div className='mb-4'>
          <h3 className='mb-1 text-lg font-semibold'>
            Come utilizziamo le tue informazioni
          </h3>
          <p className='text-gray-600 ms-3'>
            Le informazioni che raccogliamo possono essere utilizzate per:
            <ul className='ps-8'>
              <li className='list-disc'>Personalizzare la tua esperienza</li>
              <li className='list-disc'>Migliorare il nostro sito web</li>
              <li className='list-disc'>Elaborare le transazioni</li>
              <li className='list-disc'>Inviare email periodiche</li>
            </ul>
          </p>
        </div>

        <div className='mb-4'>
          <h3 className='mb-1 text-lg font-semibold'>
            Protezione delle informazioni
          </h3>
          <p className='text-gray-600 ms-3'>
            Implementiamo una varietà di misure di sicurezza per mantenere la
            sicurezza delle tue informazioni personali. Utilizziamo crittografia
            avanzata per proteggere i dati sensibili trasmessi online.
          </p>
        </div>
      </div>
    </div>
  );
};

export default PrivacyPolicyPage;

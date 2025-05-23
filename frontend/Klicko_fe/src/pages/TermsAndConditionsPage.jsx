import React from 'react';

const TermsAndConditionsPage = () => {
  return (
    <div className='max-w-5xl min-h-screen px-6 mx-auto mt-6 mb-8 xl:px-0'>
      <h1 className='mb-3 text-3xl font-bold'>Termini e Condizioni</h1>

      <div className='px-6 py-5 bg-white border rounded-lg shadow border-gray-400/30'>
        <div className='mb-4'>
          <h3 className='mb-1 text-lg font-semibold'>1. Introduzione</h3>
          <p className='text-gray-600 ms-3'>
            Benvenuto su Klicko. Utilizzando il nostro sito web, accetti questi
            termini e condizioni nella loro interezza. Non continuare a
            utilizzare il nostro sito web se non accetti tutti i termini e le
            condizioni indicati in questa pagina.
          </p>
        </div>

        <div className='mb-4'>
          <h3 className='mb-1 text-lg font-semibold'>2. Licenza d'uso</h3>
          <p className='text-gray-600 ms-3'>
            A meno che non sia indicato diversamente, Klicko e/o i suoi
            licenziatari possiedono i diritti di proprietà intellettuale per
            tutto il materiale su questo sito. Tutti i diritti di proprietà
            intellettuale sono riservati.
          </p>
        </div>

        <div className='mb-4'>
          <h3 className='mb-1 text-lg font-semibold'>3. Limitazioni</h3>
          <p className='text-gray-600 ms-3'>
            Non devi:
            <ul className='ps-8'>
              <li className='list-disc'>
                Ripubblicare il materiale da questo sito web
              </li>
              <li className='list-disc'>
                Vendere, noleggiare o concedere in sublicenza il materiale
              </li>
              <li className='list-disc'>
                Riprodurre, duplicare o copiare il materiale
              </li>
            </ul>
          </p>
        </div>

        <div className='mb-4'>
          <h3 className='mb-1 text-lg font-semibold'>4. Responsabilità</h3>
          <p className='text-gray-600 ms-3'>
            In nessun caso Avventura Mondo, né i suoi dirigenti, direttori e
            dipendenti saranno responsabili per qualsiasi perdita o danno
            inclusi, senza limitazione, perdita o danno indiretto o
            consequenziale.
          </p>
        </div>
      </div>
    </div>
  );
};

export default TermsAndConditionsPage;

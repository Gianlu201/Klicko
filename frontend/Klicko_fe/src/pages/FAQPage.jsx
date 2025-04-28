import React from 'react';
import Accordion from '../components/ui/Accordion';

const FAQPage = () => {
  const faqs = [
    {
      id: 1,
      question: `Come posso prenotare un'esperienza?`,
      answer: `Puoi prenotare un'esperienza navigando nel nostro catalogo, selezionando quella che ti interessa e cliccando sul pulsante 'Prenota'. Segui poi le istruzioni per completare il pagamento.`,
    },
    {
      id: 2,
      question: `Posso cancellare una prenotazione?`,
      answer: `Sì, puoi cancellare una prenotazione fino a 48 ore prima dell'esperienza. Per farlo, vai nella sezione 'I miei ordini' nel tuo profilo e segui le istruzioni di cancellazione.`,
    },
    {
      id: 3,
      question: `Come funziona il pagamento?`,
      answer: `Accettiamo pagamenti tramite carte di credito/debito. Tutti i pagamenti sono processati in modo sicuro e riceverai una conferma via email.`,
    },
    {
      id: 4,
      question: `Cosa succede se un'esperienza viene cancellata?`,
      answer: `In caso di cancellazione da parte nostra, riceverai un rimborso completo e ti aiuteremo a trovare un'alternativa adatta alle tue esigenze.`,
    },
    {
      id: 5,
      question: `Le esperienze sono assicurate?`,
      answer: `Sì, tutte le nostre esperienze includono un'assicurazione base. Per alcuni tipi di attività potrebbe essere richiesta un'assicurazione aggiuntiva.`,
    },
  ];

  return (
    <div className='max-w-7xl min-h-[80vh] mx-auto mb-8 mt-6'>
      <h1 className='text-3xl font-semibold mb-6'>Domande Frequenti</h1>

      <div className='flex flex-col gap-4'>
        {faqs.map((faq) => (
          <Accordion
            key={faq.id}
            intestation={<p className='cursor-pointer'>{faq.question}</p>}
            className='border border-gray-400/40 rounded-lg'
          >
            <p>{faq.answer}</p>
          </Accordion>
        ))}
      </div>
    </div>
  );
};

export default FAQPage;

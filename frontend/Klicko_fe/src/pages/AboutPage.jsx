import React from 'react';
import GroupPhoto from '/assets/images/aboutUs/groupImg.jpg';
import { Award, Earth, Heart, Users } from 'lucide-react';

const AboutPage = () => {
  const values = [
    {
      id: 1,
      title: 'Sostenibilità',
      description:
        'Ci impegniamo a minimizzare il nostro impatto ambientale e a promuovere pratiche responsabili in ogni esperienza.',
      icon: <Earth className='w-8 h-8 text-primary' />,
    },
    {
      id: 2,
      title: 'Eccellenza',
      description:
        'Selezioniamo solo le migliori esperienze per garantire ai nostri clienti ricordi indimenticabili e di alta qualità.',
      icon: <Award className='w-8 h-8 text-secondary' />,
    },
    {
      id: 3,
      title: 'Inclusività',
      description:
        'Crediamo che le avventure debbano essere accessibili a tutti e lavoriamo per creare esperienze adatte a ogni esigenza.',
      icon: <Users className='w-8 h-8 text-green-600' />,
    },
    {
      id: 4,
      title: 'Passione',
      description: `Il nostro amore per l'esplorazione e l'avventura guida ogni decisione che prendiamo e ogni esperienza che offriamo.`,
      icon: <Heart className='w-8 h-8 text-red-600' />,
    },
  ];

  return (
    <div>
      <div className='flex flex-col items-center justify-center gap-4 px-6 py-20 xl:px-0'>
        <h1 className='text-5xl font-bold'>La nostra storia</h1>
        <p className='text-xl text-gray-500'>
          Basta un click per uscire dalla routine
        </p>
      </div>

      <div className='max-w-3xl px-6 mx-auto mb-16 lg:mb-30 xl:px-0'>
        <p className='mb-10 text-lg'>
          <span className='font-bold text-primary'>Klicko</span> nasce nel 2025
          dalla passione di un gruppo di viaggiatori instancabili che
          desideravano condividere con gli altri le loro esperienze più
          emozionanti e significative.
        </p>

        <p className='mb-10 text-lg'>
          Il nostro fondatore, Gianluca Di Diego, dopo aver esplorato più di 50
          paesi e sperimentato centinaia di avventure diverse, ha deciso di
          creare una piattaforma che permettesse a tutti di accedere facilmente
          a esperienze autentiche e memorabili, senza dover affrontare
          l'incertezza e la complessità di organizzarle da soli.
        </p>

        <p className='mb-10 text-lg'>
          Oggi, con migliaia di clienti soddisfatti e centinaia di esperienze
          curate in tutto il mondo, continuiamo a perseguire la nostra missione:
          trasformare i viaggi ordinari in avventure straordinarie, creando
          ricordi che durano tutta la vita.
        </p>

        <img
          src={GroupPhoto}
          alt='group of people photo'
          className='shadow rounded-xl'
        />
      </div>

      <div className='py-16 text-center bg-gray-100'>
        <h2 className='mb-4 text-3xl font-bold'>I nostri valori</h2>
        <p className='mb-10 text-xl text-gray-500'>
          Questi sono i principi che guidano ogni nostra azione e decisione
        </p>
        <div className='grid max-w-6xl grid-cols-1 gap-6 px-6 mx-auto sm:grid-cols-2 lg:grid-cols-4 xl:px-0'>
          {values.map((value) => (
            <div
              key={value.id}
              className='px-10 py-8 text-center bg-white shadow-lg rounded-2xl'
            >
              <div className='flex justify-center mb-4'>{value.icon}</div>
              <h4 className='mb-4 text-xl font-bold'>{value.title}</h4>
              <p className='text-gray-500'>{value.description}</p>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default AboutPage;

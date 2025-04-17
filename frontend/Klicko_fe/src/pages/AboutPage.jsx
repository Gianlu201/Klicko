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
      <div className='flex flex-col justify-center items-center gap-4 py-20'>
        <h1 className='text-5xl font-bold'>La nostra storia</h1>
        <p className='text-xl text-gray-500'>
          Basta un click per uscire dalla routine
        </p>
      </div>

      <div className='max-w-3xl mx-auto mb-30'>
        <p className='text-lg mb-10'>
          <span className='text-primary font-bold'>Klicko</span> nasce nel 2025
          dalla passione di un gruppo di viaggiatori instancabili che
          desideravano condividere con gli altri le loro esperienze più
          emozionanti e significative.
        </p>

        <p className='text-lg mb-10'>
          Il nostro fondatore, Gianluca Di Diego, dopo aver esplorato più di 50
          paesi e sperimentato centinaia di avventure diverse, ha deciso di
          creare una piattaforma che permettesse a tutti di accedere facilmente
          a esperienze autentiche e memorabili, senza dover affrontare
          l'incertezza e la complessità di organizzarle da soli.
        </p>

        <p className='text-lg mb-10'>
          Oggi, con migliaia di clienti soddisfatti e centinaia di esperienze
          curate in tutto il mondo, continuiamo a perseguire la nostra missione:
          trasformare i viaggi ordinari in avventure straordinarie, creando
          ricordi che durano tutta la vita.
        </p>

        <img
          src={GroupPhoto}
          alt='group of people photo'
          className='rounded-xl shadow'
        />
      </div>

      <div className='text-center bg-gray-100 py-16'>
        <h2 className='text-3xl font-bold mb-4'>I nostri valori</h2>
        <p className='text-xl text-gray-500 mb-10'>
          Questi sono i principi che guidano ogni nostra azione e decisione
        </p>
        <div className='max-w-6xl mx-auto grid grid-cols-4 gap-6'>
          {values.map((value) => (
            <div
              key={value.id}
              className='bg-white rounded-2xl shadow-lg text-center px-10 py-8'
            >
              <div className='flex justify-center mb-4'>{value.icon}</div>
              <h4 className='text-xl font-bold mb-4'>{value.title}</h4>
              <p className='text-gray-500'>{value.description}</p>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default AboutPage;

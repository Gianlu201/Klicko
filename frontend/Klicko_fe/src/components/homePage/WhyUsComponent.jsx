import { GraduationCap, MapPinCheckInside, Clock } from 'lucide-react';
import React from 'react';

const WhyUsComponent = () => {
  const motivations = [
    {
      id: 1,
      title: 'Esperienze selezionate',
      description:
        'Tutte le nostre avventure sono accuratamente selezionate per garantirti il massimo della qualità e della sicurezza.',
      icon: (
        <GraduationCap className='text-primary bg-primary/10 p-2.5 h-13 w-13 rounded-full' />
      ),
    },
    {
      id: 2,
      title: 'Guide esperte locali',
      description:
        'I nostri esperti locali ti guideranno alla scoperta di luoghi unici con passione e professionalità.',
      icon: (
        <MapPinCheckInside className='text-secondary bg-secondary/10 p-2.5 h-13 w-13 rounded-full' />
      ),
    },
    {
      id: 3,
      title: 'Prenotazione flessibile',
      description:
        'Cambiato i tuoi piani? Nessun problema. Offriamo politiche di cancellazione flessibili su molte delle nostre esperienze.',
      icon: (
        <Clock className='text-green-600 bg-green-600/10 p-2.5 h-13 w-13 rounded-full' />
      ),
    },
  ];

  return (
    <div className='px-6 text-center py-15 bg-linear-to-r from-blue-400/20 to-orange-400/20 xl:px-0'>
      <p className='font-semibold text-secondary'>Perché sceglierci</p>

      <h2 className='text-4xl font-bold '>Rendiamo ogni avventura speciale</h2>

      <p className='mt-8 text-gray-500'>
        Con Klicko, ogni esperienza diventa un ricordo indimenticabile
      </p>

      <p className='mb-8 text-gray-500'>
        Basta un click per uscire dalla routine
      </p>

      <div className='grid grid-cols-1 gap-10 mx-auto md:grid-cols-3 max-w-7xl'>
        {motivations.map((motivation) => (
          <div
            key={motivation.id}
            className='px-6 py-6 bg-white shadow-sm text-start rounded-2xl'
          >
            {motivation.icon}
            <h4 className='my-3 text-lg font-semibold'>{motivation.title}</h4>
            <p className='text-gray-500'>{motivation.description}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default WhyUsComponent;

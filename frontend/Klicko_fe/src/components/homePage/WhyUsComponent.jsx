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
    <div className='text-center py-15 bg-linear-to-r from-blue-400/20 to-orange-400/20 px-6 xl:px-0'>
      <p className='text-secondary font-semibold'>Perché sceglierci</p>

      <h2 className=' text-4xl font-bold'>Rendiamo ogni avventura speciale</h2>

      <p className='text-gray-500 mt-8'>
        Con Klicko, ogni esperienza diventa un ricordo indimenticabile
      </p>

      <p className='text-gray-500 mb-8'>
        Basta un click per uscire dalla routine
      </p>

      <div className='grid grid-cols-1 md:grid-cols-3 gap-10 max-w-7xl mx-auto'>
        {motivations.map((motivation) => (
          <div
            key={motivation.id}
            className='text-start bg-white py-6 px-6 rounded-2xl shadow-sm'
          >
            {motivation.icon}
            <h4 className='text-lg font-semibold my-3'>{motivation.title}</h4>
            <p className='text-gray-500'>{motivation.description}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default WhyUsComponent;

// import React, { useEffect, useState } from 'react';
import Button from '../components/ui/Button';
import airImg from '/assets/images/categories/categoryAir.avif';
import waterImg from '/assets/images/categories/categoryWater.avif';
import mountainImg from '/assets/images/categories/categoryMountain.avif';
import motorsImg from '/assets/images/categories/categoryMotors.avif';
import gastronomyImg from '/assets/images/categories/categoryGastronomy.avif';
import {
  ArrowRight,
  Gauge,
  Mountain,
  Plane,
  UtensilsCrossed,
  Waves,
} from 'lucide-react';
import { Link } from 'react-router-dom';

const CategoriesPage = () => {
  // const [categories, setCategories] = useState([]);

  // const getAllCategories = async () => {
  //   try {
  //     const response = await fetch('https://localhost:7235/api/Category', {
  //       method: 'GET',
  //       headers: {
  //         'Content-Type': 'application/json',
  //       },
  //     });
  //     if (response.ok) {
  //       const data = await response.json();
  //       console.log(data);

  //       setCategories(data.categories);
  //     } else {
  //       throw new Error('Errore nel recupero dei dati!');
  //     }
  //   } catch {
  //     console.log('Error');
  //   }
  // };

  // useEffect(() => {
  //   getAllCategories();
  // }, []);

  const categories = [
    {
      id: 1,
      title: 'Esperienze in Aria',
      description:
        'Vola alto e scopri la libertà con le nostre esperienze aeree. Dal paracadutismo, al volo in mongolfiera, fino ai giri in elicottero, ti offriamo prospettive uniche sul mondo.',
      image: airImg,
      icon: (
        <Plane className='bg-primary/20 text-primary rounded-full p-4 w-16 h-16' />
      ),
    },
    {
      id: 2,
      title: 'Esperienze in Acqua',
      description:
        'Immergiti in avventure acquatiche indimenticabili. Dal rafting adrenalinico, allo snorkeling in acque cristalline, fino alle crociere di lusso, il mondo acquatico ti aspetta.',
      image: waterImg,
      icon: (
        <Waves className='bg-cyan-600/20 text-cyan-600 rounded-full p-4 w-16 h-16' />
      ),
    },
    {
      id: 3,
      title: 'Trekking e Avventure',
      description: `Esplora la maestosità della natura con i nostri percorsi di trekking e avventure terrestri. Dalle escursioni in montagna alle esplorazioni in grotta, vivi l'emozione della scoperta.`,
      image: mountainImg,
      icon: (
        <Mountain className='bg-green-600/20 text-green-600 rounded-full p-4 w-16 h-16' />
      ),
    },
    {
      id: 4,
      title: 'Sport Motoristici',
      description: `Senti l'adrenalina scorrere con le nostre esperienze motoristiche. Dai giri in pista con supercar, alle escursioni in quad, fino alle gare di kart, velocità ed emozione garantite.`,
      image: motorsImg,
      icon: (
        <Gauge className='bg-red-600/20 text-red-600 rounded-full p-4 w-16 h-16' />
      ),
    },
    {
      id: 5,
      title: 'Esperienze Gastronomiche',
      description:
        'Delizia i tuoi sensi con viaggi culinari indimenticabili. Dalle degustazioni di vini, ai corsi di cucina con chef stellati, fino ai tour gastronomici locali, scopri sapori autentici.',
      image: gastronomyImg,
      icon: (
        <UtensilsCrossed className='bg-amber-600/20 text-amber-600 rounded-full p-4 w-16 h-16' />
      ),
    },
  ];

  return (
    <div className='max-w-7xl mx-auto mt-6'>
      <div className='text-center mb-4'>
        <h1 className='text-4xl font-bold mb-3'>Categorie di Esperienze</h1>
        <p className='text-gray-500 max-w-1/2 mx-auto'>
          Scopri il mondo attraverso le nostre esclusive categorie di
          esperienze, progettate per soddisfare ogni tipo di avventuriero.
        </p>
      </div>

      <div className='mb-4'>
        {categories.map((category) => (
          <div key={category.id} className='flex items-center gap-20 my-16'>
            <div
              className={`w-1/2 aspect-16/9 rounded-3xl overflow-hidden shadow-xl ${
                category.id % 2 == 0 ? 'order-2' : 'order-1'
              }`}
            >
              <img
                src={category.image}
                alt={category.title}
                className={`w-full h-full duration-700 ease-in-out hover:scale-125`}
              />
            </div>

            <div
              className={`w-1/2 ${
                category.id % 2 == 0 ? 'order-1' : 'order-2'
              }`}
            >
              {category.icon}
              <h2 className='text-3xl font-bold my-6'>{category.title}</h2>
              <p className='text-gray-500 text-xl mb-6'>
                {category.description}
              </p>
              <Button
                variant='primary'
                icon={<ArrowRight className='w-4 h-4' />}
                iconPosition='right'
              >
                Scopri di più
              </Button>
            </div>
          </div>
        ))}
      </div>

      <div className='flex flex-col items-center text-center mb-10'>
        <h2 className='text-3xl font-bold mb-3'>Non sai da dove iniziare?</h2>
        <p className='text-gray-500 max-w-1/3 mb-3'>
          Sfoglia tutte le nostre esperienze e trova quella perfetta per te,
          indipendentemente dalla categoria.
        </p>
        <Button variant='primary'>
          <Link to='/experiences'>Esplora tutte le esperienze</Link>
        </Button>
      </div>
    </div>
  );
};

export default CategoriesPage;

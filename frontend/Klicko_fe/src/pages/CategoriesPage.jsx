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
import { Link, useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { setSelectedCategoryName } from '../redux/actions';

const CategoriesPage = () => {
  const dispatch = useDispatch();

  const navigate = useNavigate();

  const categories = [
    {
      id: 1,
      title: 'Esperienze in Aria',
      categoryName: 'Aria',
      description:
        'Vola alto e scopri la libertà con le nostre esperienze aeree. Dal paracadutismo, al volo in mongolfiera, fino ai giri in elicottero, ti offriamo prospettive uniche sul mondo.',
      image: airImg,
      icon: (
        <Plane className='bg-primary/20 text-primary rounded-full mt-1 lg:mt-0 p-1.5 lg:p-4 w-8 h-8 lg:w-16 lg:h-16' />
      ),
    },
    {
      id: 2,
      title: 'Esperienze in Acqua',
      categoryName: 'Acqua',
      description:
        'Immergiti in avventure acquatiche indimenticabili. Dal rafting adrenalinico, allo snorkeling in acque cristalline, fino alle crociere di lusso, il mondo acquatico ti aspetta.',
      image: waterImg,
      icon: (
        <Waves className='bg-cyan-600/20 text-cyan-600 rounded-full mt-1 lg:mt-0 p-1.5 lg:p-4 w-8 h-8 lg:w-16 lg:h-16' />
      ),
    },
    {
      id: 3,
      title: 'Trekking e Avventure',
      categoryName: 'Trekking',
      description: `Esplora la maestosità della natura con i nostri percorsi di trekking e avventure terrestri. Dalle escursioni in montagna alle esplorazioni in grotta, vivi l'emozione della scoperta.`,
      image: mountainImg,
      icon: (
        <Mountain className='bg-green-600/20 text-green-600 rounded-full mt-1 lg:mt-0 p-1.5 lg:p-4 w-8 h-8 lg:w-16 lg:h-16' />
      ),
    },
    {
      id: 4,
      title: 'Sport Motoristici',
      categoryName: 'Motori',
      description: `Senti l'adrenalina scorrere con le nostre esperienze motoristiche. Dai giri in pista con supercar, alle escursioni in quad, fino alle gare di kart, velocità ed emozione garantite.`,
      image: motorsImg,
      icon: (
        <Gauge className='bg-red-600/20 text-red-600 rounded-full mt-1 lg:mt-0 p-1.5 lg:p-4 w-8 h-8 lg:w-16 lg:h-16' />
      ),
    },
    {
      id: 5,
      title: 'Esperienze Gastronomiche',
      categoryName: 'Gastronomia',
      description:
        'Delizia i tuoi sensi con viaggi culinari indimenticabili. Dalle degustazioni di vini, ai corsi di cucina con chef stellati, fino ai tour gastronomici locali, scopri sapori autentici.',
      image: gastronomyImg,
      icon: (
        <UtensilsCrossed className='bg-amber-600/20 text-amber-600 rounded-full mt-1 lg:mt-0 p-1.5 lg:p-4 w-8 h-8 lg:w-16 lg:h-16' />
      ),
    },
  ];

  return (
    <div className='px-6 mx-auto mt-6 max-w-7xl xl:px-0'>
      <div className='mb-4 text-center'>
        <h1 className='mb-3 text-3xl font-bold md:text-4xl'>
          Categorie delle Esperienze
        </h1>
        <p className='max-w-sm mx-auto text-gray-500 md:max-w-md lg:max-w-xl'>
          Scopri il mondo attraverso le nostre esclusive categorie di
          esperienze, progettate per soddisfare ogni tipo di avventuriero.
        </p>
      </div>

      <div className='mb-4'>
        {categories.map((category) => (
          <div key={category.id} className='items-center gap-20 my-16 md:flex'>
            <div
              className={`mb-4 md:mb-0 md:w-3/5 lg:w-1/2 aspect-16/9 rounded-3xl overflow-hidden shadow-xl ${
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
              className={`md:w-2/5 lg:w-1/2 ${
                category.id % 2 == 0 ? 'order-1' : 'order-2'
              }`}
            >
              <div className='flex items-center justify-start gap-2 md:block'>
                {category.icon}
                <h2 className='my-6 text-3xl font-bold md:text-2xl lg:text-3xl md:my-4'>
                  {category.title}
                </h2>
              </div>
              <p className='mb-6 text-xl text-gray-500 md:text-base lg:text-lg xl:text-xl'>
                {category.description}
              </p>
              <Button
                variant='primary'
                icon={<ArrowRight className='w-4 h-4' />}
                iconPosition='right'
                onClick={() => {
                  dispatch(setSelectedCategoryName(category.categoryName));
                  navigate('/experiences');
                }}
              >
                Scopri di più
              </Button>
            </div>
          </div>
        ))}
      </div>

      <div className='flex flex-col items-center mb-10 text-center'>
        <h2 className='mb-3 text-3xl font-bold'>Non sai da dove iniziare?</h2>
        <p className='max-w-sm mb-3 text-gray-500 md:max-w-md'>
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

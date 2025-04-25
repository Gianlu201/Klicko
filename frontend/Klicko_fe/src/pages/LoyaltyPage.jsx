import { Gift, Star, Trophy } from 'lucide-react';
import React, { useEffect, useState } from 'react';
import Button from '../components/ui/Button';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';

const LoyaltyPage = () => {
  const [fidelityCard, setFidelityCard] = useState(null);
  const [fidelityLevel, setFidelityLevel] = useState(null);
  const [nextLevel, setNextLevel] = useState(null);
  const [style, setStyle] = useState({});

  const profile = useSelector((state) => state.profile);

  const navigate = useNavigate();

  const levels = [
    {
      id: 1,
      name: 'Bronzo',
      minPoints: 0,
      benefit: 'Accesso al programma fedeltà',
      mainBgColor: 'bg-amber-700',
    },
    {
      id: 2,
      name: 'Argento',
      minPoints: 500,
      benefit: 'Accesso anticipato a nuove esperienze',
      mainBgColor: 'bg-slate-400',
    },
    {
      id: 3,
      name: 'Oro',
      minPoints: 1000,
      benefit: 'Spedizione gratuita',
      mainBgColor: 'bg-amber-400',
    },
    {
      id: 4,
      name: 'Platino',
      minPoints: 2500,
      benefit: 'Assistenza clienti prioritaria',
      mainBgColor: 'bg-teal-500',
    },
    {
      id: 5,
      name: 'Diamante',
      minPoints: 5000,
      benefit: 'Esperienze esclusive riservate',
      mainBgColor: 'bg-blue-500',
    },
  ];

  const bonusList = [
    {
      id: 1,
      value: 5,
      points: 500,
    },
    {
      id: 2,
      value: 10,
      points: 950,
    },
    {
      id: 3,
      value: 20,
      points: 1800,
    },
  ];

  const getFidelityCard = async () => {
    try {
      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const response = await fetch(
        `https://localhost:7235/api/FidelityCard/getFidelityCardById/${profile.fidelityCardId}`,
        {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${token}`,
          },
        }
      );
      if (response.ok) {
        const data = await response.json();

        console.log(data);

        setFidelityCard(data.fidelityCard);
        getFidelityLevel(data.fidelityCard.points);
      } else {
        throw new Error();
      }
    } catch {
      console.log('Error');
    }
  };

  const convertPoints = async (points) => {
    try {
      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const body = {
        points: points,
      };

      const response = await fetch(
        `https://localhost:7235/api/FidelityCard/convertPointsInCoupon/${profile.fidelityCardId}`,
        {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${token}`,
          },
          body: JSON.stringify(body),
        }
      );
      if (response.ok) {
        const data = await response.json();

        console.log(data);
        getFidelityCard();
      } else {
        throw new Error();
      }
    } catch {
      console.log('Error');
    }
  };

  const formatCardNumber = (cardNumber) => {
    return cardNumber.replace(/(.{4})/g, '$1 ').trim();
  };

  const getFidelityLevel = (points) => {
    let level = null;

    levels.forEach((element) => {
      console.log(element.minPoints);
      if (points >= element.minPoints) {
        level = element;
      }
    });

    setFidelityLevel(level);
    getNextLevel(level);
  };

  const getNextLevel = (currentLevel) => {
    var index = levels.indexOf(currentLevel);

    if (index === levels.length - 1) {
      setNextLevel(null);
    } else {
      setNextLevel(levels[index + 1]);
    }
  };

  const calculateNextLevelPointsPercent = () => {
    const gap = nextLevel.minPoints - fidelityLevel.minPoints;

    const levelPoints = fidelityCard.points - fidelityLevel.minPoints;

    let percentual = Math.floor((levelPoints * 100) / gap);

    const result = 'w-[' + percentual + '%]';
    console.log(result);
    return result;
  };

  const handleMouseMove = (e) => {
    const card = e.currentTarget;
    const rect = card.getBoundingClientRect();
    const x = e.clientX - rect.left;
    const y = e.clientY - rect.top;

    const centerX = rect.width / 2;
    const centerY = rect.height / 2;

    const rotateX = -(y - centerY) / 10;
    const rotateY = (x - centerX) / 10;

    setStyle({
      transform: `perspective(600px) rotateX(${rotateX}deg) rotateY(${rotateY}deg)`,
      transition: 'transform 0.1s ease',
    });
  };

  const handleMouseLeave = () => {
    setStyle({
      transform: `perspective(600px) rotateX(0deg) rotateY(0deg)`,
      transition: 'transform 0.3s ease',
    });
  };

  useEffect(() => {
    getFidelityCard();
  }, [profile]);

  return (
    <div className='max-w-7xl mx-auto min-h-screen'>
      <h1 className='text-4xl font-bold mt-10 mb-3'>Programma Fedeltà</h1>

      {fidelityCard === null ? (
        <div className='bg-white py-8 border border-gray-400/30 rounded-xl shadow mt-6'>
          <p className='text-2xl text-gray-500 text-center font-semibold'>
            Carta fedeltà non trovata!
          </p>
        </div>
      ) : (
        <>
          {/* fidelity card */}
          <div
            className={`bg-gradient-to-br from-black/30 to-transparent text-white rounded-xl p-6 shadow-lg max-w-md w-full mx-auto mb-10 ${fidelityLevel.mainBgColor}`}
            style={style}
            onMouseMove={handleMouseMove}
            onMouseLeave={handleMouseLeave}
          >
            <div className='flex justify-between items-start mb-6'>
              <div>
                <h3 className='text-xs uppercase tracking-wider opacity-80'>
                  Klicko
                </h3>
                <h2 className='text-2xl font-semibold'>Carta Fedeltà</h2>
              </div>
              <div className='inline-flex items-center rounded-full border px-2.5 py-0.5 text-xs font-semibold transition-colors border-transparent text-primary-foreground bg-white/20 hover:bg-white/30'>
                {fidelityLevel.name || 'unknown'}
              </div>
            </div>

            <div className='mb-6'>
              <p className='text-xs uppercase tracking-wide opacity-80 mb-1'>
                Numero carta
              </p>
              <p className='text-lg slashed-zero'>
                {formatCardNumber(fidelityCard.cardNumber)}
              </p>
            </div>

            <div className='flex justify-between'>
              <div>
                <p className='text-xs uppercase tracking-wide opacity-80 mb-1'>
                  Titolare
                </p>
                <p className='font-medium'>
                  {fidelityCard.user.firstName} {fidelityCard.user.lastName}
                </p>
              </div>
              <div className='text-right'>
                <p className='text-xs uppercase tracking-wide opacity-80 mb-1'>
                  Punti
                </p>
                <p className='font-bold text-lg'>{fidelityCard.points}</p>
              </div>
            </div>
          </div>

          {/* main section */}
          <div className='grid grid-cols-12 gap-8 mb-12'>
            <div className='col-span-8 bg-white border border-gray-400/30 rounded-2xl px-6 py-5 shadow h-fit'>
              <h2 className='flex justify-start items-center gap-2 text-3xl font-semibold mb-2'>
                <Trophy className='mt-1 text-primary' />
                Il tuo status
              </h2>
              <p className='text-gray-500 text-sm font-medium mb-4'>
                Guadagna punti con ogni acquisto e accedi a benefici esclusivi
              </p>

              <div className='mb-6'>
                <div className='flex justify-between items-center mb-2'>
                  <div className='flex justify-start items-center gap-2'>
                    <div
                      className={`rounded-full p-2 ${fidelityLevel.mainBgColor}`}
                    ></div>
                    {fidelityLevel.name || 'unknown'}
                  </div>
                  <p className='text-lg font-bold'>
                    {fidelityCard.points} punti
                  </p>
                </div>

                {/* barra range livello */}
                {nextLevel !== null && (
                  <div className='rounded-full w-full mb-4 bg-slate-300/80 overflow-hidden'>
                    <div
                      className={`py-1.5 bg-green-400 ${
                        nextLevel !== null && calculateNextLevelPointsPercent()
                      }`}
                    ></div>
                  </div>
                )}

                {nextLevel !== null && (
                  <p className='text-sm text-gray-500 font-medium'>
                    Ti mancano {nextLevel.minPoints - fidelityCard.points} punti
                    per raggiungere il livello {nextLevel.name} e ottenere nuovi
                    benefici
                  </p>
                )}
              </div>

              <div className='grid grid-cols-2 gap-6 mb-2'>
                <div className='bg-gray-100 rounded-xl p-6'>
                  <h4 className='text-xl font-semibold mb-2'>
                    Punti disponibili
                  </h4>
                  <p className='text-xl font-black'>
                    {fidelityCard.availablePoints}
                  </p>
                </div>
                {nextLevel !== null && (
                  <div className='bg-gray-100 rounded-xl p-6'>
                    <h4 className='text-xl font-semibold mb-2'>
                      Prossimo livello tra
                    </h4>
                    <p className='text-xl font-black'>
                      {nextLevel.minPoints - fidelityCard.points} punti
                    </p>
                  </div>
                )}
              </div>
            </div>

            {/* converti punti */}
            <div className='col-span-4 bg-white border border-gray-400/30 rounded-2xl px-6 py-5 shadow'>
              <h2 className='flex justify-start items-center gap-2 text-3xl font-semibold mb-2'>
                <Gift className='mt-1 text-primary' />
                Riscatta punti
              </h2>
              <p className='text-gray-500 text-sm font-medium mb-6'>
                Converti i tuoi punti in sconti per i tuoi prossimi acquisti!
              </p>

              <div>
                {bonusList.map((bonus) => (
                  <div
                    key={bonus.id}
                    className='bg-gray-100 border border-gray-400/20 rounded-xl px-5 py-4 flex justify-between items-center mb-4'
                  >
                    <div>
                      <p className='text-lg font-medium'>
                        Sconto {bonus.value}€
                      </p>
                      <p className='text-sm text-gray-500'>
                        {bonus.points} punti
                      </p>
                    </div>

                    <Button
                      variant='primary'
                      disabled={
                        fidelityCard.availablePoints < bonus.points
                          ? true
                          : false
                      }
                      onClick={
                        fidelityCard.availablePoints > bonus.points &&
                        (() => {
                          convertPoints(bonus.points);
                        })
                      }
                    >
                      Riscatta
                    </Button>
                  </div>
                ))}
              </div>
            </div>
          </div>

          {/* livelli */}
          <div className='bg-white border border-gray-400/30 rounded-2xl px-6 py-5 mb-10 shadow'>
            <h2 className='flex justify-start items-center gap-2 text-3xl font-semibold mb-2'>
              <Star className='mt-1 text-primary' />
              Livelli e Benefici
            </h2>
            <p className='text-gray-500 text-sm mb-4'>
              Scopri i vantaggi di ogni livello
            </p>

            <table className='w-full'>
              <thead>
                <tr className='grid grid-cols-24 gap-4 border-b border-gray-400/30 pb-3'>
                  <th className='col-span-8 font-semibold text-start'>
                    Livello
                  </th>
                  <th className='col-span-8 font-semibold text-start'>
                    Punti richiesti
                  </th>
                  <th className='col-span-8 font-semibold text-start'>
                    Benefici speciali
                  </th>
                </tr>
              </thead>

              <tbody>
                {levels.map((level) => (
                  <tr
                    key={level.id}
                    className='grid grid-cols-24 gap-4 items-center hover:bg-gray-100 border-b border-gray-400/30 py-3 px-2 last-of-type:border-0'
                  >
                    <td className='col-span-8 flex justify-start items-center gap-2'>
                      <div
                        className={`rounded-full p-2 ${level.mainBgColor}`}
                      ></div>
                      {level.name}
                    </td>
                    <td className='col-span-8'>{level.minPoints}</td>
                    <td className='col-span-8'>{level.benefit}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </>
      )}
    </div>
  );
};

export default LoyaltyPage;

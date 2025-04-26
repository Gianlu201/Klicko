// import { CardElement, useStripe, useElements } from '@stripe/react-stripe-js';
// import { useState } from 'react';
// import Button from './ui/Button';

// const CheckoutForm = ({ sendOrder, orderAmount }) => {
//   const stripe = useStripe();
//   const elements = useElements();
//   const [message, setMessage] = useState('');

//   const [isProcessing, setIsProcessing] = useState(false);
//   const [isCardComplete, setIsCardComplete] = useState(false);

//   const CARD_ELEMENT_OPTIONS = {
//     style: {
//       base: {
//         color: '#000',
//         fontSize: '16px',
//         fontFamily: 'inherit',
//         '::placeholder': {
//           color: '#a0aec0',
//         },
//         padding: '12px 16px',
//       },
//       invalid: {
//         color: '#e53e3e',
//       },
//     },
//     hidePostalCode: true,
//   };

//   const handleSubmit = async (e) => {
//     e.preventDefault();

//     if (!stripe || !elements || !isCardComplete) {
//       return;
//     }

//     setIsProcessing(true);

//     // 1. Crea il PaymentMethod
//     const { error: paymentMethodError, paymentMethod } =
//       await stripe.createPaymentMethod({
//         type: 'card',
//         card: elements.getElement(CardElement),
//       });

//     if (paymentMethodError) {
//       setMessage(paymentMethodError.message);
//       return;
//     }

//     // 2. Richiesta al backend per creare la PaymentIntent
//     const response = await fetch(
//       'https://localhost:7235/api/Payments/create-payment-intent',
//       {
//         method: 'POST',
//         headers: { 'Content-Type': 'application/json' },
//         body: JSON.stringify({ amount: Math.floor(orderAmount * 100) }), // es: 19,99€ => 1999 centesimi
//       }
//     );

//     const { clientSecret } = await response.json();

//     console.log(`clientSecret: ${clientSecret}`);

//     // 3. Conferma il pagamento su Stripe
//     const { error: confirmError, paymentIntent } =
//       await stripe.confirmCardPayment(clientSecret, {
//         payment_method: paymentMethod.id,
//       });

//     console.log(`paymentIntent: ${paymentIntent}`);

//     if (confirmError) {
//       setMessage(confirmError.message);
//       return;
//     }

//     if (paymentIntent.status === 'succeeded') {
//       // 4. Il pagamento è riuscito!

//       setMessage('Pagamento riuscito!');

//       setIsProcessing(false);

//       // (FACOLTATIVO) 5. Fai un'altra fetch al tuo backend per salvare l'ordine
//       sendOrder();
//     } else {
//       setMessage('Pagamento non riuscito. Riprovare.');
//     }
//   };

//   const handleCardChange = (event) => {
//     setIsCardComplete(event.complete);
//   };

//   return (
//     <form onSubmit={handleSubmit} className='w-full pb-6'>
//       {/* Nome sulla carta */}
//       <div className='flex flex-col gap-2 my-5'>
//         <label htmlFor='name' className='text-sm font-medium'>
//           Nome sulla carta
//         </label>
//         <input
//           type='text'
//           id='name'
//           className='bg-background border border-gray-400/40 rounded-lg px-3 py-1.5'
//           placeholder='Mario Rossi'
//         />
//       </div>

//       {/* Numero carta con CardElement */}
//       <div className='flex flex-col gap-2 my-5'>
//         <label htmlFor='cardNumber' className='text-sm font-medium'>
//           Numero carta
//         </label>
//         <div className='bg-background border border-gray-400/40 rounded-lg px-3 py-1.5'>
//           <CardElement
//             id='cardNumber'
//             options={CARD_ELEMENT_OPTIONS}
//             onChange={handleCardChange}
//           />
//         </div>
//       </div>

//       {/* Bottone di pagamento */}
//       <Button
//         type='submit'
//         variant='primary'
//         fullWidth={true}
//         disabled={!stripe || !isCardComplete || isProcessing}
//       >
//         {isProcessing ? 'Elaborazione...' : 'Paga'}
//       </Button>

//       {/* Messaggio di errore/successo */}
//       {message && (
//         <p className='text-center text-sm text-red-600 mt-4'>{message}</p>
//       )}
//     </form>

//     // <form onSubmit={handleSubmit}>
//     //   <CardElement />
//     //   <button type='submit' disabled={!stripe}>
//     //     Paga
//     //   </button>
//     //   <p>{message}</p>
//     // </form>
//   );
// };

// export default CheckoutForm;

import {
  CardNumberElement,
  CardExpiryElement,
  CardCvcElement,
  useStripe,
  useElements,
} from '@stripe/react-stripe-js';
import { useState } from 'react';
import Button from './ui/Button';

const CheckoutForm = ({ sendOrder, orderAmount }) => {
  const stripe = useStripe();
  const elements = useElements();
  const [message, setMessage] = useState('');

  const [isProcessing, setIsProcessing] = useState(false);
  const [isCardNumberComplete, setIsCardNumberComplete] = useState(false);
  const [isExpiryComplete, setIsExpiryComplete] = useState(false);
  const [isCvcComplete, setIsCvcComplete] = useState(false);

  const CARD_ELEMENT_OPTIONS = {
    style: {
      base: {
        color: '#000',
        fontSize: '16px',
        fontFamily: 'inherit',
        '::placeholder': {
          color: '#a0aec0',
        },
        padding: '12px 16px',
      },
      invalid: {
        color: '#e53e3e',
      },
    },
  };

  const CARD_NUMBER_OPTIONS = {
    autoComplete: 'off',
    style: {
      base: {
        color: '#000',
        fontSize: '16px',
        fontFamily: 'inherit',
        '::placeholder': {
          color: '#a0aec0',
        },
        padding: '12px 16px',
      },
      invalid: {
        color: '#e53e3e',
      },
    },
    placeholder: '4242 4242 4242 4242',
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (
      !stripe ||
      !elements ||
      !isCardNumberComplete ||
      !isExpiryComplete ||
      !isCvcComplete
    ) {
      return;
    }

    setIsProcessing(true);

    // 1. Crea il PaymentMethod
    const cardNumberElement = elements.getElement(CardNumberElement);

    const { error: paymentMethodError, paymentMethod } =
      await stripe.createPaymentMethod({
        type: 'card',
        card: cardNumberElement,
      });

    if (paymentMethodError) {
      setMessage(paymentMethodError.message);
      setIsProcessing(false);
      return;
    }

    // 2. Chiamata al backend per ottenere il clientSecret
    const response = await fetch(
      'https://localhost:7235/api/Payments/create-payment-intent',
      {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ amount: Math.floor(orderAmount * 100) }),
      }
    );

    const { clientSecret } = await response.json();

    // 3. Conferma il pagamento
    const { error: confirmError, paymentIntent } =
      await stripe.confirmCardPayment(clientSecret, {
        payment_method: paymentMethod.id,
      });

    if (confirmError) {
      setMessage(confirmError.message);
      setIsProcessing(false);
      return;
    }

    if (paymentIntent.status === 'succeeded') {
      setMessage('Pagamento riuscito!');
      setIsProcessing(false);
      sendOrder(); // Chiamo la funzione passata da props
    } else {
      setMessage('Pagamento non riuscito. Riprovare.');
      setIsProcessing(false);
    }
  };

  const handleCardNumberChange = (event) => {
    setIsCardNumberComplete(event.complete);
  };

  const handleExpiryChange = (event) => {
    setIsExpiryComplete(event.complete);
  };

  const handleCvcChange = (event) => {
    setIsCvcComplete(event.complete);
  };

  return (
    <form onSubmit={handleSubmit} className='w-full pb-6' autoComplete='nope'>
      {/* Nome sulla carta */}
      <div className='flex flex-col gap-2 my-5'>
        <label htmlFor='name' className='text-sm font-medium'>
          Nome sulla carta
        </label>
        <input
          type='text'
          id='name'
          className='bg-background border border-gray-400/40 rounded-lg px-3 py-1.5'
          placeholder='Mario Rossi'
        />
      </div>

      {/* Numero carta */}
      <div className='flex flex-col gap-2 my-5'>
        <label htmlFor='cardNumber' className='text-sm font-medium'>
          Numero carta
        </label>
        <div className='bg-background border border-gray-400/40 rounded-lg px-3 py-1.5'>
          <CardNumberElement
            id='cardNumber'
            options={CARD_NUMBER_OPTIONS}
            onChange={handleCardNumberChange}
          />
        </div>
      </div>

      {/* Scadenza e CVV */}
      <div className='flex justify-between items-start gap-6 my-5'>
        {/* Scadenza */}
        <div className='w-full flex flex-col gap-2'>
          <label htmlFor='cardExpiry' className='text-sm font-medium'>
            Scadenza
          </label>
          <div className='bg-background border border-gray-400/40 rounded-lg px-3 py-1.5'>
            <CardExpiryElement
              id='cardExpiry'
              options={CARD_ELEMENT_OPTIONS}
              onChange={handleExpiryChange}
            />
          </div>
        </div>

        {/* CVC */}
        <div className='w-full flex flex-col gap-2'>
          <label htmlFor='cardCvc' className='text-sm font-medium'>
            CVV
          </label>
          <div className='bg-background border border-gray-400/40 rounded-lg px-3 py-1.5'>
            <CardCvcElement
              id='cardCvc'
              options={CARD_ELEMENT_OPTIONS}
              onChange={handleCvcChange}
            />
          </div>
        </div>
      </div>

      {/* Bottone di pagamento */}
      <Button
        type='submit'
        variant='primary'
        fullWidth={true}
        disabled={
          !stripe ||
          !isCardNumberComplete ||
          !isExpiryComplete ||
          !isCvcComplete ||
          isProcessing
        }
      >
        {isProcessing
          ? 'Elaborazione...'
          : `Paga ${orderAmount.toFixed(2).replace('.', ',')} €`}
      </Button>

      {/* Messaggio di errore/successo */}
      {message && (
        <p className='text-center text-sm text-red-600 mt-4'>{message}</p>
      )}
    </form>
  );
};

export default CheckoutForm;

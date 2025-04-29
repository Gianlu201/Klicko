import {
  CardNumberElement,
  CardExpiryElement,
  CardCvcElement,
  useStripe,
  useElements,
} from '@stripe/react-stripe-js';
import { useState } from 'react';
import Button from './ui/Button';
import { X } from 'lucide-react';
import { toast } from 'sonner';

const CheckoutForm = ({ sendOrder, orderAmount }) => {
  const stripe = useStripe();
  const elements = useElements();
  const [errorMessage, setErrorMessage] = useState('');
  const [successMessage, setSuccessMessage] = useState('');

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
    try {
      e.preventDefault();

      setErrorMessage('');
      setSuccessMessage('');

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

      const cardNumberElement = elements.getElement(CardNumberElement);

      const { error: paymentMethodError, paymentMethod } =
        await stripe.createPaymentMethod({
          type: 'card',
          card: cardNumberElement,
        });

      if (paymentMethodError) {
        setErrorMessage(paymentMethodError.message);
        setIsProcessing(false);
        return;
      }

      const response = await fetch(
        'https://localhost:7235/api/Payments/create-payment-intent',
        {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({ amount: Math.floor(orderAmount * 100) }),
        }
      );

      const { clientSecret } = await response.json();

      const { error: confirmError, paymentIntent } =
        await stripe.confirmCardPayment(clientSecret, {
          payment_method: paymentMethod.id,
        });

      if (confirmError) {
        setErrorMessage(confirmError.message);
        setIsProcessing(false);
        return;
      }

      if (paymentIntent.status === 'succeeded') {
        setSuccessMessage('Pagamento riuscito!');
        setIsProcessing(false);
        toast.success('Pagamento effettuato con successo!');
        sendOrder();
      } else {
        setErrorMessage('Pagamento non riuscito. Riprovare.');
        setIsProcessing(false);
      }
    } catch (e) {
      console.log(e.Message);
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

      {/* Messaggio di errore */}
      {errorMessage && (
        <p className='relative text-center text-sm font-medium text-red-600 bg-red-200/70 py-3 mt-4 rounded-xl'>
          {errorMessage}
          <X
            className='absolute top-1 right-1.5 z-40 w-5 h-5 cursor-pointer'
            onClick={() => setErrorMessage('')}
          />
        </p>
      )}

      {/* Messaggio di successo */}
      {successMessage && (
        <p className='relative text-center text-sm font-medium text-green-600 bg-green-200/70 py-3 mt-4 rounded-xl'>
          {successMessage}
          <X
            className='absolute top-1 right-1.5 z-40 w-5 h-5 cursor-pointer'
            onClick={() => setSuccessMessage('')}
          />
        </p>
      )}
    </form>
  );
};

export default CheckoutForm;

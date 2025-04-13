import { jwtDecode } from 'jwt-decode';
import { toast } from 'sonner';

export const SET_LOGGED_USER = 'SET_LOGGED_USER';
export const LOGOUT = 'LOGOUT';
export const SET_USER_CART = 'SET_USER_CART';
export const CART_MODIFIED = 'CART_MODIFIED';
export const SET_EXPERIENCES_LIST = 'SET_EXPERIENCES_LIST';
export const SET_CATEGORIES_LIST = 'SET_CATEGORIES_LIST';

export const setLoggedUser = (data) => {
  const tokenDecoded = jwtDecode(data.token);

  const userInfos = {
    aud: tokenDecoded.aud,
    exp: tokenDecoded.exp,
    role: tokenDecoded[
      'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
    ],
    email: tokenDecoded.email,
    name: tokenDecoded.name,
    surname: tokenDecoded.surname,
    id: tokenDecoded.nameidentifier,
    cartId: tokenDecoded.cartId,
    iss: tokenDecoded.iss,
    expiration: data.expires,
  };

  // console.log(userInfos);

  toast.success(`Bentornato ${userInfos.name}`);

  return {
    type: SET_LOGGED_USER,
    payload: userInfos,
  };
};

export const logoutUser = () => {
  localStorage.removeItem('klicko_token');
  return {
    type: LOGOUT,
  };
};

export const setUserCart = (cart) => {
  const experiences = [];

  if (cart.cartExperiences !== null) {
    cart.cartExperiences.forEach((exp) => {
      experiences.push({
        title: exp.title,
        categoryName: exp.category.name,
        coverImage: exp.coverImage,
        experienceId: exp.experienceId,
        isFreeCancellable: exp.isFreeCancellable,
        place: exp.place,
        duration: exp.duration,
        price: exp.price,
        quantity: exp.quantity,
        sale: exp.sale,
      });
    });
  }

  const userCart = {
    cartId: cart.cartId,
    createdAt: cart.createdAt,
    updatedAt: cart.updatedAt,
    experiences: experiences,
    modified: false,
  };

  return {
    type: SET_USER_CART,
    payload: userCart,
  };
};

export const cartModified = () => ({
  type: CART_MODIFIED,
});

export const setExperiencesList = (experiencesList) => ({
  type: SET_EXPERIENCES_LIST,
  payload: experiencesList,
});

export const setCategoriesList = (categoriesList) => ({
  type: SET_CATEGORIES_LIST,
  payload: categoriesList,
});

// export const defaultUrl = 'http://localhost/TutorsOfMogilev';
export const defaultUrl = 'http://localhost:5000';
export const logoutUrl = '/Account/Logout';
// api urls

const apiUrl = `${defaultUrl}/api`;

export const TUTORS_URL = `${apiUrl}/tutors`;
export const TUTORS_SUBJECTS_ADDITIONAL_URL = `/subjects`;
export const TUTORS_SPECIALIZATIONS_ADDITIONAL_URL = `/specializations`;
export const SUBJECTS_URL = `${apiUrl}/subjects`;
export const CONTACT_TYPES_URL = `${apiUrl}/contact-types`;
export const SPECIALIZATIONS_URL = `${apiUrl}/specializations`;
export const CITIES_URL = `${apiUrl}/cities`;
export const PHONES_URL = `${apiUrl}/phones`;
export const CONTACTS_URL = `${apiUrl}/contacts`;

// action types

export const GRID_STATE_CHANGE_ACTION = 'GRID_STATE_CHANGE';

export const CLOSE_ERROR_DIALOG = 'CLOSE_DIALOG';

export const TUTOR = 'TUTOR';
export const SUBJECT = 'SUBJECT';
export const CONTACT_TYPE = 'CONTACT_TYPE';
export const SPECIALIZATION = 'SPECIALIZATION';
export const CITY = 'CITY';
export const PHONE = 'PHONE';
export const CONTACT = 'CONTACT';

export const LOAD_LIST = '_LOAD_LIST';
export const SAVE_QUERY_PARAMS = 'SAVE_QUERY_PARAMS';
export const CREATE_ITEM = '_CREATE_ITEM';
export const UPDATE_ITEM = '_UPDATE_ITEM';
export const REMOVE_ITEM = '_REMOVE_ITEM';

export const SET_FILTER = '_SET_FILTER';

export const UPDATE_TUTORS_SUBJECTS = '_UPDATE_TUTORS_SUBJECTS';
export const UPDATE_TUTORS_SPECIALIZATIONS = '_UPDATE_TUTORS_SPECIALIZATIONS';

export const START = '_START';
export const SUCCESS = '_SUCCESS';
export const FAIL = '_FAIL';

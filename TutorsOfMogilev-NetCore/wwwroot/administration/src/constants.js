// export const defaultUrl = 'http://localhost/TutorsOfMogilev';
export const defaultUrl = 'http://localhost:5000';
// api urls

const apiUrl = `${defaultUrl}/api`;

export const TUTORS_URL = `${apiUrl}/tutors`;
export const SUBJECTS_URL = `${apiUrl}/subjects`;
export const CONTACT_TYPES_URL = `${apiUrl}/contact-types`;
export const SPECIALIZATIONS_URL = `${apiUrl}/specializations`;
export const DISTRICTS_URL = `${apiUrl}/districts`;

// action types

export const OPEN_DELETE_CONFIRM_DIALOG = 'OPEN_DELETE_CONFIRM_DIALOG';
export const CLOSE_DELETE_CONFIRM_DIALOG = 'CLOSE_DELETE_CONFIRM_DIALOG';
export const OPEN_DIALOG_WITH_INPUT = 'OPEN_DIALOG_WITH_INPUT';
export const CLOSE_DIALOG_WITH_INPUT = 'CLOSE_DIALOG_WITH_INPUT';
export const OPEN_ERROR_DIALOG = 'OPEN_ERROR_DIALOG';
export const CLOSE_ERROR_DIALOG = 'CLOSE_ERROR_DIALOG';

export const SET_ERROR_MESSAGE = 'SET_ERROR_MESSAGE';

export const TUTOR = 'TUTOR';
export const SUBJECT = 'SUBJECT';
export const CONTACT_TYPE = 'CONTACT_TYPE';
export const SPECIALIZATION = 'SPECIALIZATION';
export const DISTRICT = 'DISTRICT';

export const LOAD_LIST = '_LOAD_LIST';
export const CREATE_ITEM = '_CREATE_ITEM';
export const UPDATE_ITEM = '_UPDATE_ITEM';
export const REMOVE_ITEM = '_REMOVE_ITEM';

export const START = '_START';
export const SUCCESS = '_SUCCESS';
export const FAIL = '_FAIL';

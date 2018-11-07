import {
  TUTOR,
  TUTORS_URL,
  LOAD_LIST,
  CREATE_ITEM,
  UPDATE_ITEM,
  REMOVE_ITEM} from '../constants';

export const loadTutors = () => ({
  type: TUTOR + LOAD_LIST,
  get: TUTORS_URL
});

export const createTutor = tutor => ({
  type: TUTOR + CREATE_ITEM,
  payload: { data: tutor },
  post: TUTORS_URL
});

export const updateTutor = (id, tutor) => ({
  type: TUTOR + UPDATE_ITEM,
  payload: { id, data: tutor },
  put: TUTORS_URL
});

export const removeTutor = id => ({
  type: TUTOR + REMOVE_ITEM,
  payload: { id },
  del: TUTORS_URL
});
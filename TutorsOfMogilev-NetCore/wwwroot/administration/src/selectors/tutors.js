import { createSelector } from 'reselect';

const getTutors = store => store.tutors.items;

export const getRowsForGrid = createSelector(
  [getTutors],
  tutors =>
    tutors.map(tutor => ({
      id: tutor.id,
      firstName: tutor.firstName,
      lastName: tutor.lastName,
      patronymic: tutor.patronymic,
      education: tutor.education,
      job: tutor.job,
      address: tutor.address,
      rating: tutor.rating,
      isVisible: tutor.isVisible,
      cost: tutor.cost,
      description: tutor.description,
      experience: tutor.experience,
      photo: tutor.photoPath,
      district: tutor.district
    }))
);

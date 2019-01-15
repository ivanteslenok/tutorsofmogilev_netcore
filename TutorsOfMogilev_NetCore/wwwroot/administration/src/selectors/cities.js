import _ from 'lodash';
import { createSelector } from 'reselect';

const getCities = store => store.cities.items;

export const getSortedCities = createSelector(
  [getCities],
  (cities) => _.sortBy(cities, distr => distr['name'])
);

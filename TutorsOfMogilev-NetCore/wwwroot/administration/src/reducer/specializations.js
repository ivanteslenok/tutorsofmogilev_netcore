import { SPECIALIZATION } from '../constants';
import reducerWithCrud from '../decorators/reducerWithCrud';

const specializations = reducerWithCrud(SPECIALIZATION);
export default specializations;

import { File } from './file.interface';
export interface CMM {
  id?: string;
  requestedBy?: string;
  entity?: string;
  published: boolean;
  file?: File;
  customer?: string;
  cmmNumber?: string;
  initialDate?: Date;
  model?: string;
  manualRev?: string;
  revDate?: Date;
  aircraft?: string;
  jobNo?: string;
  vendor?: string;
  engineer?: string;
  rfq?: string;
  pm?: string;
  incorporatedSeatAssemblies?: string[];
  serviceBulletins?: string[];
  reference?: string[];
  aircraftInstallation?: string;
  config?: string;
  trimFinish?: string;
  comments?: string;
  documentAvailable?: string;
  roles?: string[];
  seatPartNumbers?: string[];
  seatPartNumbersTSO?: string[];
  unId?: string;
  convertedDate?: Date;
  available: boolean
}

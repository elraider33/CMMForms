import {File} from './file.interface';
export interface Bulletin {
  id: string;
  requestedBy?: string;
  requestedOn: Date;
  
  entity?: string;
  published: boolean;
  file?: File;
  sbno?: string;
  type?: string;
  modelNumber?: string;
  initialDate: Date;
  customer?: string;
  manualRev?: string;
  revDate: Date;
  description?: string;
  model?: string;
  aircraft?: string;
  unid?: string;
  manual?: string;
  cmm?: string[];
  jobNumber?: string;
  repairStationNumber?: string;
  seatPartNumbers: string[];
  comments?: string;
  roles: string[];
  convertedDate?: Date;
  companycode?: string[];
  view?: boolean;
  sbSearch?: string;
}

export interface NavItem {
  displayName: string;
  disabled?: boolean;
  iconName: string;
  route?: Array<string>;
  children?: NavItem[];
  onlyAdmin?: boolean;
  expanded?: boolean;
}

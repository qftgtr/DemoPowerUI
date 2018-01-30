//--------------------------------------//               PowerUI////        For documentation or //    if you have any issues, visit//        powerUI.kulestar.com////    Copyright � 2013 Kulestar Ltd//          www.kulestar.com//--------------------------------------using System;namespace Css{		/// <summary>	/// Describes if an element is valid	/// <summary>	sealed class IsValid:CssKeyword{				public override string Name{			get{				return "valid";			}		}				public override SelectorMatcher GetSelectorMatcher(){			return new ValidMatcher();		}			}		/// <summary>	/// Handles the matching process for :valid.	/// </summary>		sealed class ValidMatcher:LocalMatcher{				public override bool TryMatch(Dom.Node node){						if(node==null){				return false;			}						if(node["required"]==null){				// Not required.				return true;			}						// Get the value:			string value=node["value"];						// Not blank:			return ( !string.IsNullOrEmpty(value) );		}			}	}
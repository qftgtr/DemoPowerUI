//--------------------------------------
//             InfiniText
//
//        For documentation or 
//    if you have any issues, visit
//        powerUI.kulestar.com
//
//    Copyright � 2013 Kulestar Ltd
//          www.kulestar.com
//--------------------------------------

using System;
using Blaze;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace InfiniText{
	
	/// <summary>A raw glyph in the font. With this you can access the raw outline (as it's also a vector) and stats about this glyph.
	/// Its size is relative; it's stored as if it's a 1px font.</summary>
	
	public partial class Glyph:VectorPath,AtlasEntity{
		
		/// <summary>The colour used to fill the glyphs.</summary>
		private static Color32 Fill=new Color32(255,255,255,255);
		
		/// <summary>A globally unique ID for this glyph. Used with font atlases.</summary>
		public int UniqueID;
		/// <summary>The font face this is a glyph from.</summary>
		public FontFace Font;
		/// <summary>The first charcode to assign itself to this glyph.</summary>
		public int RawCharcode;
		/// <summary>The amount to advance by when this glyph is displayed.</summary>
		public float AdvanceWidth;
		/// <summary>The left side bearing. An offset of this glyph.</summary>
		public float LeftSideBearing;
		/// <summary>A group of kerning pairs and their offsets.</summary>
		public Dictionary<Glyph,float> Kerning;
		
		
		/// <summary>Does this glyph require loading? If this is true, call LoadNow to get the glyph outline loaded.</summary>
		public virtual bool RequiresLoad{
			get{
				return (PathNodeCount==1 && FirstPathNode.Unloaded);
			}
		}
		
		/// <summary>Add this glyph to the named font. Can override existing characters.</summary>
		/// <param name="name">The font family name, e.g. "Vera".</param>
		public bool AddToFont(string name,int charcode){
			
			// Get the font family:
			FontFamily family=Fonts.Get(name);
			
			if(family==null){
				return false;
			}
			
			Font=family.Regular;
			
			if(Font==null){
				return false;
			}
			
			// Add it as the given charcode:
			AddCharcode(charcode);
			
			return true;
			
		}
		
		/// <summary>Add this glyph to the named font. Can override existing characters.</summary>
		/// <param name="name">The font family name, e.g. "Vera".</param>
		public bool AddToFont(string name,string charcode){
			
			if(charcode==null || charcode.Length==0){
				return false;
			}
			
			return AddToFont(name,(int)charcode[0]);
			
		}
		
		/// <summary>Completes the load of this glyph. You must have checked RequiresLoad first.</summary>
		public virtual void LoadNow(){
			
			// Get the meta:
			LoadMetaPoint meta=FirstPathNode as LoadMetaPoint;
			
			// Clear:
			FirstPathNode=null;
			LatestPathNode=null;
			PathNodeCount=0;
			
			if(Font.CffParser!=null){
				
				// Use the CFF parser:
				Font.CffParser.LoadFully(this,meta);
				
			}else{
				
				// Glyph table load:
				GlyfTables.LoadFully(this,Font.Parser,meta);
				
			}
			
			// Reduce the amount of unloaded glyphs:
			Font.UnloadedGlyphs--;
			
			if(Font.UnloadedGlyphs<=0){
				// Let the font know that every glyph is now loaded:
				Font.AllGlyphsLoaded();
			}
			
		}
		
		public bool MultiThreadDraw(){
			// When drawn to an atlas, the draw function can be used on another thread.
			return true;
		}
		
		public void GetDimensionsOnAtlas(out int width,out int height){
			
			// Meta (bounds):
			if(Width==0f){
				RecalculateBounds();
			}
			
			width=RasterWidth;
			height=RasterHeight;
			
		}
		
		public bool DrawToAtlas(TextureAtlas atlas,AtlasLocation location){
			
			float offsetX=Fonts.SdfOffset-MinX;
			float offsetY=Fonts.SdfOffset-MinY;
			
			TextureCameras.RequestDraw(location,this,offsetX,offsetY,Fonts.SdfPixelHeight);
			return true;
			
		}
		
		public int GetAtlasID(){
			return UniqueID;
		}
		
		/// <summary>The height of this glyphs SDF raster.</summary>
		public int RasterHeight{
			get{
				
				// How tall will the raster be?
				int height=(int)(Height * Fonts.SdfPixelHeight);
				
				// Add the SDF "blur" space:	
				height+=Fonts.DoubleSdfSize;
				
				return height;
				
			}
		}
		
		/// <summary>The width of this glyphs SDF raster.</summary>
		public int RasterWidth{
			get{
				
				// How wide will the raster be? Height is correct here.
				int width=(int)(Width * Fonts.SdfPixelHeight);
				
				// Add the SDF "blur" space:
				width+=Fonts.DoubleSdfSize;
				
				return width;
				
			}
		}
		
		public bool Rasterise(Color32[] atlasPixels,int atlasWidth,int baseIndex,bool clear){
			
			// How wide will the raster be? Height is correct here.
			int width=(int)(Width * Fonts.SdfPixelHeight);
			
			// How tall will it be?
			int height=(int)(Height * Fonts.SdfPixelHeight);
			
			return Fonts.Rasteriser.Rasterise(this,atlasPixels,atlasWidth,baseIndex,width,height,-MinX,-MinY,Fill,clear);
			
		}
		
		public bool Rasterise(Color32[] atlasPixels,int atlasWidth,int baseIndex,bool clear,Color32 colour){
			
			// How wide will the raster be? Height is correct here.
			int width=(int)(Width * Fonts.SdfPixelHeight);
			
			// How tall will it be?
			int height=(int)(Height * Fonts.SdfPixelHeight);
			
			return Fonts.Rasteriser.Rasterise(this,atlasPixels,atlasWidth,baseIndex,width,height,-MinX,-MinY,colour,clear);
			
		}
		
		public Glyph(){
			Fonts.GlyphID++;
			UniqueID=Fonts.GlyphID;
		}
		
		public Glyph(FontFace parent):this(){
			
			Font=parent;
			
		}
		
		public Glyph Copy(){
			
			// Create a new glyph:
			Glyph glyph=new Glyph(Font);
			
			Copy(glyph);
			
			return glyph;
			
		}
		
		/// <summary>Copies this glyphs info into the given one. Does not copy Unique ID or RawCharcode.</summary>
		public void Copy(Glyph into){
			
			// Glyph properties:
			into.HoleSorted=WasHoleSorted;
			into.Width=Width;
			into.AdvanceWidth=AdvanceWidth;
			into.LeftSideBearing=LeftSideBearing;
			
			// Copy actual path into it:
			CopyInto(into);
			
		}
		
		public void AddKerningPair(Glyph beforeThis,float value){
			
			// When beforeThis is before this, this is kerned by the given value.
			
			if(Kerning==null){
				
				// Create the dictionary:
				Kerning=new Dictionary<Glyph,float>();
				
			}
			
			// Push:
			Kerning[beforeThis]=value;
			
		}
		
		public void AddCharcode(int charCode){
			
			if(charCode==0){
				// Ignore .notdef
				return;
			}
			
			if(RawCharcode==0){
				RawCharcode=charCode;
			}
			
			// Hook it up for the given charcode:
			Font.Glyphs[charCode]=this;
			
		}
		
		/// <summary>How much this glyph extends below the baseline.</summary>
		public float DescendorOffset{
			get{
				return MinX;
			}
		}
		
		/// <summary>Gets this glyphs charcode as a string. Note that this is a string and not a char because of things called surrogate pairs.</summary>
		public string TextString{
			get{
				return char.ConvertFromUtf32(RawCharcode);
			}
		}
		
		/// <summary>The first charcode assigned to this glyph.</summary>
		public int Charcode{
			get{
				return RawCharcode;
			}
		}
		
		public virtual bool IsComposite{
			get{
				return false;
			}
		}
		
		public virtual void LoadFully(Glyph[] glyphs){}
		
	}

}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum HairType
{
	SHair = 1,
	MHair = 2,
	LHair = 3,
}
public enum SpecialItemType
{
	None = 0,
	Gold = 1,
	Diamond = 2,
	Energy = 3,
	Stamina = 4,
	ShopExp = 5,
	RoleExp = 6,
	WishCoin = 7,
	ShopCoin = 8,
}
public enum PartType
{
	Heent = 1,
	Hair = 2,
	Skin = 3,
}
public enum ItemTabs
{
	ALL = 0,//全部
	Dress = 1,//服装
	Prop = 2,//道具
	Piece = 3,//材料碎片
	Reel = 4,//图纸
	Heent = 5,//五官
	HeentPiece = 6,//五官碎片
}

public enum ItemType
{
	/// <summary>
	/// 全部
	/// </summary>
	All = 0,
	/// <summary>
	/// 材料
	/// </summary>
	Piece = 1,
	/// <summary>
	/// 图纸
	/// </summary>
	Reel = 2,
	/// <summary>
	/// 礼物
	/// </summary>
	Gift = 3,
	/// <summary>
	/// 精品
	/// </summary>
	Boutique = 4,
	/// <summary>
	/// 服装
	/// </summary>
	Dress = 5,
	/// <summary>
	/// 五官
	/// </summary>
	Heent = 6,
	/// <summary>
	/// 五官碎片
	/// </summary>
	HeentPiece = 7,
}

public enum RareLevel
{
	/// <summary>
	/// 普通物品
	/// </summary>
	Normal = 0,
	/// <summary>
	/// 珍稀
	/// </summary>
	Rare = 1,
	/// <summary>
	/// 超珍稀
	/// </summary>
	SuperRare = 2,
}
public enum EDressType
{
	Null = 0,
	Suit = 1,
	Hair = 2,
	Dress = 3,
	Shirt = 4,
	Trousers = 5,//长裤
	Coat = 6,//外套
	Shoes = 7,
	Hat = 8,//帽子
	Bag = 9,
	Ornaments = 10,//装饰
	Belt = 11,
	Socks = 12,//短袜
	Special = 13, //特殊饰品
	Skin = 14,
}

public enum EDressStyle
{
	All = 0,
	Sennv = 1,
	Yinglun = 3,
	Fugu = 4,
	Bailing = 5,
	Xiha = 6,
	Gete = 7,
	Yaogun = 8,
	Boximiye = 9,
	Xiuxian = 10,
	Lifu = 11,
	Xiaoqingxin = 12,
	KeAi = 21,
	YouYa = 22,
	LangMan = 23,
	ShiShang = 24,
	RouMei = 25,
	HuaLi = 26,
}

public enum EDressDetailType
{
	None = 0,
	Suit = 1,
	Hair,
	Eye,
	Nose,
	Mouth,
	Eyebrow,
	ShortSkirt,
	LongSkirt,
	FullSkirt,
	Shirt,
	ShortPants,
	LongPants,
	Coat,
	Shoes,
	Hat,
	Bag,
	Bracelet,
	Necklace,
	Headwear,
	Belt,
	Socks,
	Earing,
	Face = 23,
	ShirtM = 24,
	SwimwearTop = 25,
	SwimwearDwn = 26,
	SwimwearFull = 27,
	Background = 28,    //后景
	UnderwearTop = 29,  //内衣上衣
	UnderwearDwn = 30,  //内衣下装
	UnderwearFull = 31, //内衣一套
	Backwear = 32,      //背饰
	Facewear = 33,      //脸饰

	Skin = 40,
}
/// <summary>
/// 五官的种类
/// </summary>
public enum EFaceItemType
{
	Eye = 3,
	Nose = 4,
	Mouth = 5,
	Eyebrow = 6
}
public enum EDressSliceType
{
	HairFront = 1,
	HairBack,
	EyeLeft,
	EyeRight,
	EyebrowLeft,
	EyebrowRight,
	Nose,
	Mouth,
	ShirtBody,
	CoatBody,
	WaistHip,
	BigArmLeft,
	ForeArmLeft,
	LeftHand,
	BigArmRight,
	ForeArmRight,
	RightHand,
	LeftThigh,
	LeftShank,
	LeftFoot,
	RightThigh,
	RightShank,
	RightFoot,
	Hat,
	Bag,
	LeftEarring,
	RightEarring
}

public class DressTypeHelper
{

	//twoLevelDirectory
	//ThreeLevelDirectories

	private static Dictionary<int, int[]> dressTypeToDetailsDic = new Dictionary<int, int[]>()
	{
		{(int)EDressType.Ornaments,new int[]{(int)EDressDetailType.Necklace,(int)EDressDetailType.Bracelet,(int)EDressDetailType.Headwear,(int)EDressDetailType.Earing}},   //装饰品
        {(int)EDressType.Special,new int[]{(int)EDressDetailType.Facewear,(int)EDressDetailType.Background,(int)EDressDetailType.Backwear,(int)EDressDetailType.UnderwearDwn,(int)EDressDetailType.UnderwearTop,(int)EDressDetailType.UnderwearFull}},                                //特殊装饰
    };

	public static Dictionary<int, int[]> DressTypeToDetailsDic {
		get { return DressTypeHelper.dressTypeToDetailsDic; }
	}

	public static bool IsUnderwear(EDressDetailType detailType)
	{
		if (detailType == EDressDetailType.UnderwearDwn || detailType == EDressDetailType.UnderwearTop || detailType == EDressDetailType.UnderwearFull)
		{
			return true;
		}
		else return false;
	}

	public static bool IsTwoLevelDressType(EDressType dressType)
	{
		if (dressTypeToDetailsDic.ContainsKey((int)dressType))
		{
			return true;
		}
		else return false;
	}

	public static int[] GetTwoLevelDressType(EDressType dressType)
	{
		if (IsTwoLevelDressType(dressType))
		{
			return dressTypeToDetailsDic[(int)dressType];
		}
		else return null;
	}

}
